using DartParser.Dart.Objects.BaseTypes;

namespace DartParser.Dart.Objects.Singletons;

public class DartNull : DartObject
{
    private DartNull() : base("null", ClassId.kNullCid) { }

    public static DartNull Instance { get; } = new DartNull();
}
