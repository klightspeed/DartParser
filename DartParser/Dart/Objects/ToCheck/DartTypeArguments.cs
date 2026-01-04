using DartParser.Dart;
using DartParser.Dart.Objects.BaseTypes;

namespace DartParser.Dart.Objects.ToCheck;

public class DartTypeArguments(ulong count) : DartInstance(ClassId.kTypeArgumentsCid)
{
    public ulong Count { get; set; } = count;
    public ulong Count2 { get; set; }
    public ulong Hash { get; set; }
    public ulong Nullability { get; set; }
    public DartArray? Instantiations { get; set; }
    public List<DartAbstractType?> Types { get; set; } = [];
}
