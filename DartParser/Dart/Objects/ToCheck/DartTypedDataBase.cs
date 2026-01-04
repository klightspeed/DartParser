using DartParser.Dart;

namespace DartParser.Dart.Objects.ToCheck;

public class DartTypedDataBase(ClassId cid) : DartTypedData(cid)
{
    public DartTypedDataBase() : this(ClassId.kTypedDataBaseCid) { }

    public DartInteger? Length { get; set; }
}
