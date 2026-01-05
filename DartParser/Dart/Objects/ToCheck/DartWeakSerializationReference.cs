using DartParser.Dart;
using DartParser.Dart.Objects.BaseTypes;

namespace DartParser.Dart.Objects.ToCheck;

public class DartWeakSerializationReference() : DartObject(ClassId.kWeakSerializationReferenceCid)
{
    public DartObject? Target { get; set; }
    public DartObject? Replacement { get; set; }
}
