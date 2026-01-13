using DartParser.Dart.Objects.BaseTypes;

namespace DartParser.Dart.Objects.Singletons;

public class DartBool(bool value) : DartObject(ClassId.kBoolCid)
{
    public bool Value { get; } = value;
}
