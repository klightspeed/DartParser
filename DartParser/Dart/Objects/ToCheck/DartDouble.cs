using DartParser.Dart;
using DartParser.Dart.Objects;
using Semver;
namespace DartParser.Dart.Objects.ToCheck;

public class DartDouble() : DartObject(ClassId.kDoubleCid), IDartObject<DartDouble>
{
    public double Value { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartDouble> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddValue(e => e.Value);
    }
}
