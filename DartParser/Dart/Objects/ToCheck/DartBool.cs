using DartParser.Dart;
using DartParser.Dart.Objects;

namespace DartParser.Dart.Objects.ToCheck;

public class DartBool(bool value) : DartObject(ClassId.kBoolCid)
{
    public bool Value { get; } = value;

    public static DartBool True { get; } = new(true);
    public static DartBool False { get; } = new(false);
}
