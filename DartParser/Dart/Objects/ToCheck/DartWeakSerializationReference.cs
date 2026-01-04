using DartParser.Dart;
using DartParser.Dart.Objects;

namespace DartParser.Dart.Objects.ToCheck;

public class DartWeakSerializationReference() : DartObject(ClassId.kWeakSerializationReferenceCid)
{
    [DartField]
    public DartObject? Target { get; set; }

    [DartField]
    public DartObject? Replacement { get; set; }
}
