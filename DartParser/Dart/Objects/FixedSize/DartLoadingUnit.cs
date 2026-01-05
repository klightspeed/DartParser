using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartLoadingUnit() : DartObject(ClassId.kLoadingUnitCid), IHasPropertySetters<DartLoadingUnit>
{
    public DartLoadingUnit? Parent { get; set; }
    public ulong Id { get; set; }

    public List<DartLibrary> Libraries { get; } = [];

    public static void InitPropertySetters(DartPropertySetters<DartLoadingUnit> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.Parent);
        setters.AddValue(e => e.Id);
    }
}
