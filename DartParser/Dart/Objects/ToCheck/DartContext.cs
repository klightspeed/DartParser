using DartParser.Dart;
using DartParser.Dart.Objects;

namespace DartParser.Dart.Objects.ToCheck;

public class DartContext() : DartObject(ClassId.kContextCid)
{
    public int NumVariables { get; set; }

    [DartField]
    public DartContext? Parent { get; set; }
}
