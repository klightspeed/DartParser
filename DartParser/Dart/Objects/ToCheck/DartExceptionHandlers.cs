using DartParser.Dart;
using DartParser.Dart.Objects;

namespace DartParser.Dart.Objects.ToCheck;

public class DartExceptionHandlers() : DartObject(ClassId.kExceptionHandlersCid)
{
    [DartField]
    public DartArray? HandledTypesData { get; set; }
}
