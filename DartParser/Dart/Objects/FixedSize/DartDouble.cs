using DartParser.Dart.Objects.BaseTypes;
using Semver;
using System.Diagnostics;
namespace DartParser.Dart.Objects.FixedSize;

[DebuggerDisplay("{Type} {Value}")]
public class DartDouble() : DartNumber(ClassId.kDoubleCid), IHasPropertySetters<DartDouble>
{
    public double Value { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartDouble> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddValue(e => e.Value);
    }
}
