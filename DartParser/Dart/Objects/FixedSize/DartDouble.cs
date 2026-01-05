using DartParser.Dart.Objects.BaseTypes;
using Semver;
namespace DartParser.Dart.Objects.FixedSize;

public class DartDouble() : DartNumber(ClassId.kDoubleCid), IHasPropertySetters<DartDouble>
{
    public double Value { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartDouble> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddValue(e => e.Value);
    }
}
