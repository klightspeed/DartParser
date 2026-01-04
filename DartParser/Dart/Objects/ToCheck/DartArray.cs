using DartParser.Dart;
using DartParser.Dart.Objects;

namespace DartParser.Dart.Objects.ToCheck;

public class DartArray(ClassId cid) : DartObject(cid)
{
    public DartArray() : this(ClassId.kArrayCid) { }

    public DartTypeArguments? TypeArguments { get; set; }
    public ulong Length { get; set; }
    public List<DartObject?> Data { get; set; } = [];

    public static new DartArray Null { get; } = new DartArray(ClassId.kNullCid);
}
