using DartParser.Dart.Objects.BaseTypes;

namespace DartParser.Dart.Objects.Singletons;

public class DartNull : DartObject
{
    public DartNull() : base("null", ClassId.kNullCid) { }
}
