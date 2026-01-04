using DartParser.Dart;
using DartParser.Dart.Objects;
using DartParser.Dart.Objects.ToCheck;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartClosureData() : DartObject(ClassId.kClosureDataCid), IDartObject<DartClosureData>
{
    public DartContextScope? ContextScope { get; set; }
    public DartFunction? ParentFunction { get; set; }
    public DartClosure? Closure { get; set; }
    public ulong PackedFields { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartClosureData> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        if (kind != SnapshotKind.kFullAOT) setters.AddRef(e => e.ContextScope);

        setters.AddRef(e => e.ParentFunction);
        setters.AddRef(e => e.Closure);
        setters.AddValue(e => e.PackedFields);
    }
}
