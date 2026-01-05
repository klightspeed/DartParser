using System.Numerics;

namespace DartParser.Dart.Objects.BaseTypes;

public class DartTypedDataBase(ClassId cid) : DartTypedData(cid)
{
    public DartTypedDataBase() : this(ClassId.kTypedDataBaseCid) { }

    public ulong Length { get; set; }

    public virtual void SetEntry<T>(int index, T value)
        where T : unmanaged, IBinaryInteger<T>
    {
        throw new NotSupportedException();
    }
}
