using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.VariableLength;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartSubtypeTestCache() : DartObject(ClassId.kSubtypeTestCacheCid), IHasPropertySetters<DartSubtypeTestCache>
{
    public DartArray? Cache { get; set; }
    public ulong NumInputs { get; set; }
    public ulong NumOccupied { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartSubtypeTestCache> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.Cache);
        setters.AddValue(e => e.NumInputs);
        setters.AddValue(e => e.NumOccupied);
    }
}
