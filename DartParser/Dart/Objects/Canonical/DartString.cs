using DartParser.Dart.Objects.BaseTypes;
using System.Diagnostics;
using System.Text;

namespace DartParser.Dart.Objects.Canonical;

[DebuggerDisplay("{Type} {Value}")]
public class DartString : DartInstance
{
    public ulong Length { get; private set; }
    public uint Hash { get; private set; }

    public string? Value { get; private set; }
    public byte[]? OneByteData { get; private set; }
    public char[]? TwoByteData { get; private set; }

    public DartString(ClassId cid) : base(cid)
    {
        if (cid != ClassId.kOneByteStringCid
            && cid != ClassId.kTwoByteStringCid
            && cid != ClassId.kStringCid
            && cid != ClassId.kExternalOneByteStringCid
            && cid != ClassId.kExternalTwoByteStringCid)
        {
            throw new InvalidOperationException($"Invalid string type {cid}");
        }
    }

    public DartString(ClassId cid, string value) : this(cid)
    {
        Value = value;
        Length = (ulong)value.Length;
    }

    public DartString(ClassId cid, ulong length) : this(cid)
    {
        Length = length;
    }

    public static DartString Empty { get; } = new DartString(ClassId.kStringCid, "");

    public void Fill(Snapshot snapshot)
    {
        if (Value != null) throw new InvalidOperationException("Already initialized");
        var lengthAndCid2 = snapshot.ReadUnsigned();
        var length2 = lengthAndCid2 >> 1;
        var cid2 = (lengthAndCid2 & 1) == 0 ? ClassId.kOneByteStringCid : ClassId.kTwoByteStringCid;
        Debug.Assert(length2 == Length);
        Debug.Assert(cid2 == Type);
        Debug.Assert(Length < int.MaxValue / 2);

        if (this.Type == ClassId.kOneByteStringCid)
        {
            this.OneByteData = [.. snapshot.ReadBytes((int)Length)];
            this.Value = Encoding.Latin1.GetString(this.OneByteData);
        }
        else if (this.Type == ClassId.kTwoByteStringCid)
        {
            this.Value = Encoding.Unicode.GetString(snapshot.ReadBytes((int)Length * 2));
            this.TwoByteData = [.. this.Value];
        }
        else
        {
            throw new InvalidOperationException($"Invalid string type {this.Type}");
        }
    }

    public static DartString Create(ObjectClassIdTag tags, ClassId cid, DartStream stream)
    {
        var str = new DartString(cid);

        if (stream.Is64Bit)
        {
            str.Hash = tags.Hash;
        }
        else
        {
            str.Hash = stream.ReadRaw<uint>();
        }

        var smilen = stream.ReadRaw<UWord>().Value;
        Debug.Assert((smilen & 1) == 0);
        str.Length = smilen >> 1;

        if (cid == ClassId.kOneByteStringCid)
        {
            str.OneByteData = [.. stream.ReadBytes((int)str.Length)];
            str.Value = Encoding.Latin1.GetString(str.OneByteData);
        }
        else if (cid == ClassId.kTwoByteStringCid)
        {
            str.Value = Encoding.Unicode.GetString(stream.ReadBytes((int)str.Length * 2));
            str.TwoByteData = [.. str.Value];
        }

        return str;
    }
}
