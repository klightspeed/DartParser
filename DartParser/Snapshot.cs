using DartParser;
using DartParser.Dart;
using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using DartParser.Dart.Objects.FixedSize;
using DartParser.Dart.Objects.Other;
using DartParser.Dart.Objects.ToCheck;
using DartParser.Dart.Objects.VariableLength;
using Semver;
using System.Buffers.Binary;
using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Text;

namespace DartParser;

public class Snapshot(DartIsolate isolate, byte[] snapshotData, byte[] snapshotInstructions)
{
    public DartIsolate Isolate { get; } = isolate;
    public byte[] SnapshotData { get; } = snapshotData;
    public byte[] SnapshotInstructions { get; } = snapshotInstructions;
    public bool Is64Bit => Isolate.Is64Bit;
    public bool IsBigEndian => Isolate.IsBigEndian;
    public Architecture Architecture => Isolate.Architecture;
    public bool IsVMSnapshot => Isolate.IsVmIsolate;
    public ClassTable ClassTable => Isolate.ClassTable;

    public uint Magic { get; set; }
    public ulong Length { get; set; }
    public SnapshotKind Kind { get; set; }
    public string? SnapshotVersion { get; set; }
    public string[] SnapshotFeatures { get; set; } = [];
    public ulong HeaderLength { get; set; }
    public ulong NumBaseObjects { get; set; }
    public ulong NumObjects { get; set; }
    public ulong NumClusters { get; set; }
    public ulong InstructionTableLength { get; set; }
    public ulong InstructionTableDataOffset { get; set; }
    public List<DeserializationClusterBase> Clusters { get; } = [];
    public List<DartObject> Objects { get; } = [];
    public bool UseCompressedPointers { get; } = true;
    public bool IsNonRootUnit { get; } = false;
    public SemVersion Version { get; set; } = new SemVersion(int.MaxValue);
    public DartStream Stream { get; set; } = DartStream.Empty;
    public DartInstructionsTable InstructionsTable { get; set; } = new DartInstructionsTable();
    public DartInstructionsSection InstructionsSection { get; set; } = new DartInstructionsSection();
    public ReadOnlyMemory<byte> DataImage { get; set; } = Memory<byte>.Empty;
    public Dictionary<Type, DartPropertySetters> Setters { get; set; } = [];

    public bool IsProduct => SnapshotFeatures.Contains("product");
    public bool IsOppositeEndianness => IsBigEndian == BitConverter.IsLittleEndian;

    public bool ProcessSnapshot()
    {
        Memory<byte> data = SnapshotData;
        Magic = ReadUInt32(data.Span);
        if (Magic != 0xdcdcf5f5) return false;
        Length = ReadUInt64(data.Span[4..]);
        Kind = (SnapshotKind)ReadUInt64(data.Span[12..]);
        SnapshotVersion = Encoding.Latin1.GetString(data.Span[20..52]);
        var snapshotFeaturesEnd = data.Span[52..].IndexOf((byte)0);
        if (snapshotFeaturesEnd < 0) throw new InvalidDataException();
        SnapshotFeatures = Encoding.Latin1.GetString(data.Span.Slice(52, snapshotFeaturesEnd)).Split(' ');
        HeaderLength = (ulong)(52 + snapshotFeaturesEnd + 1);
        var stream = new DartStream(data[..(int)Length], IsBigEndian, Is64Bit);
        var imageStream = new DartStream(data, IsBigEndian, Is64Bit);
        imageStream.Advance((int)Length);
        imageStream.Align(64);
        DataImage = imageStream.PendingMemory;
        stream.Advance((int)HeaderLength);
        Stream = stream;
        NumBaseObjects = stream.ReadUnsigned();
        NumObjects = stream.ReadUnsigned();
        NumClusters = stream.ReadUnsigned();
        InstructionTableLength = stream.ReadUnsigned();
        InstructionTableDataOffset = stream.ReadUnsigned();
        Clusters.EnsureCapacity((int)NumClusters);
        Objects.EnsureCapacity((int)NumObjects + 1);
        Version = VersionTable.GetVersionFromSnapshotHash(SnapshotVersion);

        ClassTable.Init(this);

        Debug.Assert((ulong)Objects.Count == this.NumBaseObjects);
        Debug.Assert(Clusters.Count == 0);

        InstructionsSection = GetInstructionsSection();
        InstructionsTable.Init(this);

        for (ulong i = 0; i < NumClusters; i++)
        {
            var cluster = DeserializationClusterBase.Create(this);
            Clusters.Add(cluster);
            Debug.Assert(ReferenceEquals(Clusters[(int)i], cluster));

            cluster.ReadAllocate(this);
        }

        Debug.Assert((ulong)Objects.Count == this.NumObjects);

        for (ulong i = 0; i < NumClusters; i++)
        {
            Clusters[(int)i].ReadFill(this);
        }

        foreach (var obj in this.Objects)
        {
            foreach (var prop in obj.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (!prop.CanWrite) continue;
                var propType = prop.PropertyType;
                var elementType = propType.HasElementType ? propType.GetElementType() : null;

                if (propType.IsPrimitive || elementType?.IsPrimitive == true) continue;
                if (prop.GetValue(obj) is not { } propVal) continue;

                switch (prop.GetValue(obj))
                {
                    case null:
                    case string:
                    case Vector128<int>:
                    case Vector128<float>:
                    case Vector128<double>:
                    case Vector128<int>[]:
                    case Vector128<float>[]:
                    case Vector128<double>[]:
                    case ClassId:
                    case DartRefId:
                    case List<DartObject>:
                    case List<(DartObject, string)>:
                    case List<DartClass>:
                    case List<DartCode>:
                        break;
                    case DartArray array:
                        array.LinkedObjects.Add((obj, prop.Name));

                        for (int i = 0; i < array.Data.Length; i++)
                        {
                            array.Data[i]?.LinkedObjects.Add((obj, $"{prop.Name}->Data[]"));
                        }

                        break;
                    case DartGrowableObjectArray array:
                        array.LinkedObjects.Add((obj, prop.Name));

                        for (int i = 0; i < array.Data?.Data.Length; i++)
                        {
                            array.Data.Data[i]?.LinkedObjects.Add((obj, $"{prop.Name}->Data->Data[]"));
                        }

                        break;
                    case DartObject linked:
                        linked.LinkedObjects.Add((obj, prop.Name));
                        break;
                    case DartObject[] list:
                        for (int i = 0; i < list.Length; i++)
                        {
                            list[i]?.LinkedObjects.Add((obj, $"{prop.Name}[]"));
                        }
                        break;
                    case IList list when elementType?.IsValueType == true:
                        for (int i = 0; i < list.Count; i++)
                        {
                            foreach (var subprop in list[i]?.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance) ?? [])
                            {
                                if (subprop.GetValue(list[i]) is DartObject subobj)
                                {
                                    subobj.LinkedObjects.Add((obj, $"{prop.Name}[].{subprop.Name}"));
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        Isolate.PopulateObjects(this);

        return true;
    }

    public void AddObject(DartObject obj)
    {
        if (obj.ObjectIndex != 0)
        {
            Debug.Assert(obj.ObjectIndex == Objects.Count + 1);
        }
        else
        {
            obj.ObjectIndex = Objects.Count + 1;
        }

        Objects.Add(obj);
    }


    #region Readers

    public void FillFields<T>(T obj)
        where T : class, IHasPropertySetters<T>
    {
        if (Setters.GetValueOrDefault(typeof(T)) is not DartPropertySetters<T> setters)
        {
            Setters[typeof(T)] = setters = new();

            T.InitPropertySetters(setters, this.Version, this.Kind, this.IsProduct);
        }

        setters.FillFields(ref obj, this);
    }

    public T FillFields<T>()
        where T : IHasPropertySetters<T>, new()
    {
        if (Setters.GetValueOrDefault(typeof(T)) is not DartPropertySetters<T> setters)
        {
            Setters[typeof(T)] = setters = new();

            T.InitPropertySetters(setters, this.Version, this.Kind, this.IsProduct);
        }

        T obj = new();

        setters.FillFields(ref obj, this);

        return obj;
    }

    public void FillFields<T>(ref T obj)
        where T : struct, IHasPropertySetters<T>
    {
        if (Setters.GetValueOrDefault(typeof(T)) is not DartPropertySetters<T> setters)
        {
            Setters[typeof(T)] = setters = new();

            T.InitPropertySetters(setters, this.Version, this.Kind, this.IsProduct);
        }

        setters.FillFields(ref obj, this);
    }

    public ulong ReadUnsigned()
        => Stream.ReadUnsigned();

    public T Read<T>()
        where T : unmanaged, IBinaryInteger<T>
        => Stream.ReadValue<T>();

    public DartRefId ReadRefId()
        => new(Stream.ReadRefId());

    public T? ReadRef<T>() where T : DartObject
    {
        var refid = checked((int)Stream.ReadRefId());
        var obj = refid == 0 ? DartObject.Null : Objects[refid - 1];
        return obj.Cast<T>();
    }

    public T ReadValue<T>() where T : unmanaged
        => Stream.ReadValue<T>();

    public ClassId ReadCid()
        => Stream.ReadCid(ClassTable);

    public ReadOnlySpan<byte> ReadBytes(int length)
        => Stream.ReadBytes(length);

    public void ReadBytes(Span<byte> buffer, int length)
        => Stream.ReadBytes(buffer, length);

    public void ReadBytes(Span<byte> buffer)
        => Stream.ReadBytes(buffer, buffer.Length);

    public T ReadRaw<T>()
        where T : struct
    {
        if (typeof(T) != typeof(Vector128<int>)
            && typeof(T) != typeof(Vector128<float>)
            && typeof(T) != typeof(Vector128<double>)
            && typeof(T) != typeof(byte)
            && typeof(T) != typeof(sbyte)
            && typeof(T) != typeof(short)
            && typeof(T) != typeof(ushort)
            && typeof(T) != typeof(int)
            && typeof(T) != typeof(uint)
            && typeof(T) != typeof(long)
            && typeof(T) != typeof(ulong)
            && typeof(T) != typeof(float)
            && typeof(T) != typeof(double))
        {
            throw new NotSupportedException();
        }

        Span<byte> span = stackalloc byte[Unsafe.SizeOf<T>()];
        ReadBytes(span);

        if (IsOppositeEndianness)
        {
            span.Reverse();
        }

        return MemoryMarshal.Read<T>(span);
    }

    public T[] ReadTypedData<T>(int length)
        where T : struct
    {
        if (typeof(T) != typeof(Vector128<int>)
            && typeof(T) != typeof(Vector128<float>)
            && typeof(T) != typeof(Vector128<double>)
            && typeof(T) != typeof(byte)
            && typeof(T) != typeof(sbyte)
            && typeof(T) != typeof(short)
            && typeof(T) != typeof(ushort)
            && typeof(T) != typeof(int)
            && typeof(T) != typeof(uint)
            && typeof(T) != typeof(long)
            && typeof(T) != typeof(ulong)
            && typeof(T) != typeof(float)
            && typeof(T) != typeof(double))
        {
            throw new NotSupportedException();
        }

        var data = new T[length];
        var span = data.AsSpan();
        ReadBytes(MemoryMarshal.AsBytes(span));

        if (IsOppositeEndianness)
        {
            for (int i = 0; i < length; i++)
            {
                MemoryMarshal.AsBytes(span.Slice(i, 1)).Reverse();
            }
        }

        return data;
    }

    public T[] ReadTypedData<T>()
        where T : struct
        => ReadTypedData<T>((int)ReadUnsigned());

    public ReadOnlyMemory<byte> GetBareInstructionsAt(int offset)
        => SnapshotInstructions.AsMemory(offset);

    public short ReadInt16(ReadOnlySpan<byte> span)
        => IsBigEndian
         ? BinaryPrimitives.ReadInt16BigEndian(span)
         : BinaryPrimitives.ReadInt16LittleEndian(span);

    public ushort ReadUInt16(ReadOnlySpan<byte> span)
        => IsBigEndian
         ? BinaryPrimitives.ReadUInt16BigEndian(span)
         : BinaryPrimitives.ReadUInt16LittleEndian(span);

    public int ReadInt32(ReadOnlySpan<byte> span)
        => IsBigEndian
         ? BinaryPrimitives.ReadInt32BigEndian(span)
         : BinaryPrimitives.ReadInt32LittleEndian(span);

    public uint ReadUInt32(ReadOnlySpan<byte> span)
        => IsBigEndian
         ? BinaryPrimitives.ReadUInt32BigEndian(span)
         : BinaryPrimitives.ReadUInt32LittleEndian(span);

    public long ReadInt64(ReadOnlySpan<byte> span)
        => IsBigEndian
         ? BinaryPrimitives.ReadInt64BigEndian(span)
         : BinaryPrimitives.ReadInt64LittleEndian(span);

    public ulong ReadUInt64(ReadOnlySpan<byte> span)
        => IsBigEndian
         ? BinaryPrimitives.ReadUInt64BigEndian(span)
         : BinaryPrimitives.ReadUInt64LittleEndian(span);

    public long ReadWord(ReadOnlySpan<byte> span)
        => Is64Bit
         ? ReadInt64(span)
         : ReadInt32(span);

    public ulong ReadUWord(ReadOnlySpan<byte> span)
        => Is64Bit
         ? ReadUInt64(span)
         : ReadUInt32(span);

    public int SizeOfUWord()
        => Is64Bit
         ? 8
         : 4;

    #endregion

    private DartInstructionsSection GetInstructionsSection()
    {
        var stream = new DartStream(SnapshotInstructions, IsBigEndian, Is64Bit);

        var imageSize = stream.ReadRaw<Word>().Value;
        var instructionSectionOffset = stream.ReadRaw<Word>().Value;

        Debug.Assert(instructionSectionOffset >= 64 && (instructionSectionOffset & (instructionSectionOffset - 1)) == 0);

        stream.Align((int)instructionSectionOffset);

        var cidTag = stream.ReadRaw<UWord>().Value;
        var cid = ClassTable.LookupClassId(cidTag >> 12);

        Debug.Assert(cid == ClassId.kInstructionsSectionCid);

        var payloadLength = stream.ReadRaw<Word>().Value;
        var bssOffset = stream.ReadRaw<Word>().Value;
        var instructionRelocationOffset = stream.ReadRaw<Word>().Value;
        var buildIdOffset = stream.ReadRaw<Word>().Value;

        stream.Align(64);

        return new DartInstructionsSection
        {
            PayloadLength = payloadLength,
            BSSOffset = bssOffset,
            InstructionsRelocatedAddress = instructionRelocationOffset,
            BuildIdOffset = buildIdOffset,
            Data = stream.ReadMemory((int)payloadLength)
        };
    }

    public DartInstructions? GetInstructionsAt(ulong offset)
    {
        var data = SnapshotInstructions.AsMemory((int)offset);
        var tags = ReadUWord(data.Span);
        var cidval = tags >> 12;
        var cid = ClassTable.LookupClassId(cidval);
        if (cid != ClassId.kInstructionsCid) return null;
        return null;
    }

    public T? GetObjectAt<T>(ulong offset)
        where T : DartObject
    {
        var stream = new DartStream(DataImage[(int)offset..], IsBigEndian, Is64Bit);
        var cidTags = stream.ReadRaw<UWord>();
        var cidval = (cidTags.Value >> 12) & 0xFFFFF;
        var cid = ClassTable.LookupClassId(cidval);

        switch (cid)
        {
            case ClassId.kOneByteStringCid:
            case ClassId.kTwoByteStringCid:
                Debug.Assert(typeof(T) == typeof(DartString));
                return (T)(DartObject)DartString.Create(cidTags, cid, stream);
        }

        throw new NotSupportedException();
    }
}
