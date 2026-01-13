using DartParser.Dart;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace DartParser;

public class DartStream(ReadOnlyMemory<byte> buffer, bool isBigEndian, bool is64Bit)
{
    const int kDataBitsPerByte = 7;
    const int kByteMask = (1 << kDataBitsPerByte) - 1;
    const int kMaxUnsignedDataPerByte = kByteMask;
    const int kMinDataPerByte = -(1 << (kDataBitsPerByte - 1));
    const int kMaxDataPerByte = (~kMinDataPerByte & kByteMask);
    const int kEndByteMarker = (byte.MaxValue - kMaxDataPerByte);
    const int kEndUnsignedByteMarker = (byte.MaxValue - kMaxUnsignedDataPerByte);

    private readonly ReadOnlyMemory<byte> Buffer = buffer;
    private ReadOnlyMemory<byte> Current = buffer;

    public bool IsBigEndian { get; } = isBigEndian;
    public bool Is64Bit { get; } = is64Bit;
    public bool IsOppositeEndianness => IsBigEndian == BitConverter.IsLittleEndian;

    public static DartStream Empty { get; } = new DartStream(ReadOnlyMemory<byte>.Empty, false, false);

    public void ReadBytes(Span<byte> buffer, int len)
    {
        ReadBytes(len).CopyTo(buffer);
    }

    public ReadOnlySpan<byte> ReadBytes(int len)
    {
        var retspan = Current.Span[..len];
        Current = Current[len..];
        return retspan;
    }

    public ReadOnlyMemory<byte> ReadMemory(int len)
    {
        var retmem = Current[..len];
        Current = Current[len..];
        return retmem;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Advance(int len)
    {
        Current = Current[len..];
    }

    public int Position
    {
        get => Buffer.Length - Current.Length;
        set => Advance(value - Position);
    }

    public int PendingBytes => Current.Length;

    public ReadOnlyMemory<byte> PendingMemory => Current;

    //[MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Align(int alignment, int offset = 0)
    {
        if (alignment <= 0) throw new ArgumentException("alignment must be greater than 0", nameof(alignment));
        if (offset < 0) throw new ArgumentException("offset must be greater than or equal to 0", nameof(offset));
        if (offset >= alignment) throw new ArgumentException("offset must be less than alignment", nameof(offset));
        if ((alignment & (alignment - 1)) != 0) throw new ArgumentException("alignment must be a power of 2", nameof(offset));
        var align = Position & (alignment - 1);
        offset -= align;
        if (offset < 0) offset += alignment;
        Advance(offset);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public byte ReadByte()
    {
        byte b = Current.Span[0];
        Current = Current[1..];
        return b;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public T ReadSLEB<T>()
        where T : unmanaged, IBinaryInteger<T>, ISignedNumber<T>
    {
        int shift = 0;
        byte b;
        T val = T.Zero;

        do
        {
            b = ReadByte();
            val |= T.CreateChecked(b & 0x7F) << shift;
            shift += 7;
        }
        while ((b & 0x80) != 0);

        T sign_bits = T.Zero;

        if ((b & 0x40) != 0)
        {
            sign_bits = T.NegativeOne << shift;
        }

        return val | sign_bits;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public T ReadLEB128<T>()
        where T : unmanaged, IBinaryInteger<T>, IUnsignedNumber<T>
    {
        int shift = 0;
        byte b;
        T val = T.Zero;

        do
        {
            b = ReadByte();
            val |= T.CreateChecked(b & 0x7F) << shift;
            shift += 7;
        }
        while ((b & 0x80) != 0);

        return val;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    private T Read<T>(byte endByteMarker)
        where T : unmanaged, IBinaryInteger<T>
    {
        if (Unsafe.SizeOf<T>() == 1)
        {
            if (T.AllBitsSet < T.Zero)
            {
                return T.CreateChecked((sbyte)ReadByte());
            }
            else
            {
                return T.CreateChecked(ReadByte());
            }
        }

        int shift = 0;
        byte b;
        T val = T.Zero;

        do
        {
            b = ReadByte();
            val |= T.CreateChecked(b) << shift;
            shift += 7;
        } while (b <= kMaxUnsignedDataPerByte);

        val -= T.CreateChecked(endByteMarker) << (shift - 7);

        return val;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public T ReadCast<T, TInt>()
        where T : unmanaged
        where TInt : unmanaged, IBinaryInteger<TInt>
    {
        return Unsafe.BitCast<TInt, T>(Read<TInt>(kEndByteMarker));
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public T ReadValue<T>()
        where T : unmanaged
    {
        return Unsafe.SizeOf<T>() switch
        {
            1 => ReadCast<T, byte>(),
            2 => ReadCast<T, ushort>(),
            4 => ReadCast<T, uint>(),
            8 => ReadCast<T, ulong>(),
            _ => throw new NotSupportedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public ulong ReadUnsigned() => Read<ulong>(kEndUnsignedByteMarker);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public ClassId ReadCid(ClassTable classTable)
    {
        return classTable.LookupClassId(ReadValue<ulong>());
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public ObjectClassIdTag ReadObjectTag(ClassTable classTable)
    {
        return classTable.DecodeObjectTag(ReadUnsigned());
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public ulong ReadRefId()
    {
        byte b;
        ulong val = 0;

        do
        {
            b = ReadByte();
            val = (val << 7) | (b & 0x7Fu);
        } while ((b & 0x80) == 0);

        return val;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void ReadBytes(Span<byte> buffer)
        => ReadBytes(buffer, buffer.Length);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public T ReadRaw<T>()
        where T : struct
        => ReadRawAligned<T>(1);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public T ReadRawAligned<T>(int align = -1)
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
            && typeof(T) != typeof(double)
            && typeof(T) != typeof(UWord)
            && typeof(T) != typeof(Word)
            && typeof(T) != typeof(ObjectClassIdTag))
        {
            throw new NotSupportedException();
        }

        int size = default(T) switch
        {
            byte or sbyte => 1,
            short or ushort => 2,
            int or uint or float => 4,
            long or ulong or double => 8,
            Vector128<int> or Vector128<float> or Vector128<double> => 16,
            Word or UWord => Is64Bit ? 8 : 4,
            _ => Unsafe.SizeOf<T>()
        };

        if (align > 0 && (align & (align - 1)) != 0)
        {
            align = -1;
        }

        align = (align, default(T)) switch
        {
            ( > 0, _) => align,
            (0, _) => 1,
            (_, byte or sbyte) => 1,
            (_, short or ushort) => 2,
            (_, int or uint or float) => 4,
            (_, long or ulong or double or Vector128<int> or Vector128<float> or Vector128<double>) => 8,
            (_, Word or UWord) => Is64Bit ? 8 : 4,
            _ => Unsafe.SizeOf<T>()
        };

        int offset = (default(T), Is64Bit, IsBigEndian) switch
        {
            (Word or UWord, false, true) => 4,
            _ => 0
        };

        Align(align);
        Span<byte> span = stackalloc byte[Unsafe.SizeOf<T>()];
        ReadBytes(span[offset..], size);

        if (IsOppositeEndianness)
        {
            span.Reverse();
        }

        return MemoryMarshal.Read<T>(span);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public T[] ReadTypedData<T>()
        where T : struct
        => ReadTypedData<T>((int)ReadUnsigned());
}
