using DartParser.Dart;
using DartParser.Dart.Objects;
using DartParser.Dart.Objects.ToCheck;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartSubtypeTestCache() : DartObject(ClassId.kSubtypeTestCacheCid), IDartObject<DartSubtypeTestCache>
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
