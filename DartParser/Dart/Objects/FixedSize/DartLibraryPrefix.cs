using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using DartParser.Dart.Objects.VariableLength;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartLibraryPrefix() : DartInstance(ClassId.kLibraryPrefixCid), IHasPropertySetters<DartLibraryPrefix>
{
    public DartString? Name { get; set; }
    public DartArray? Imports { get; set; }
    public DartLibrary? Importer { get; set; }
    public ulong NumImports { get; set; }
    public ulong IsDeferredLoad { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartLibraryPrefix> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.Name);
        setters.AddRef(e => e.Imports);

        if (kind != SnapshotKind.kFullAOT)
        {
            setters.AddRef(e => e.Importer);
        }

        setters.AddValue(e => e.NumImports);
        setters.AddValue(e => e.IsDeferredLoad);
    }
}
