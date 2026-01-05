using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartWeakProperty() : DartInstance(ClassId.kWeakPropertyCid), IHasPropertySetters<DartWeakProperty>
{
    public DartObject? Key { get; set; }
    public DartObject? Value { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartWeakProperty> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.Key);
        setters.AddRef(e => e.Value);
    }
}
