using DartParser.Dart;
using DartParser.Dart.Objects;

namespace DartParser.Dart.Objects.BaseTypes;

public class DartError(ClassId cid) : DartObject(cid)
{
    public DartError() : this(ClassId.kErrorCid) { }
}
