using DartParser.Dart;
using DartParser.Dart.Objects;
using DartParser.Dart.Objects.ToCheck;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartLibrary() : DartObject(ClassId.kLibraryCid), IDartObject<DartLibrary>
{
    public DartString? Name { get; set; }
    public DartString? Url { get; set; }
    public DartString? PrivateKey { get; set; }
    public DartArray? Dictionary { get; set; }
    public DartArray? Metadata { get; set; }
    public DartClass? TopLevelClass { get; set; }
    public DartGrowableObjectArray? UsedScripts { get; set; }
    public DartLoadingUnit? LoadingUnit { get; set; }
    public DartArray? Imports { get; set; }
    public DartArray? Exports { get; set; }
    public DartArray? Dependencies { get; set; }
    public DartKernelProgramInfo? KernelProgramInfo { get; set; }
    public DartArray? LoadedScripts { get; set; }

    public ulong Index { get; set; }
    public ulong NumImports { get; set; }
    public byte LoadState { get; set; }
    public byte Flags { get; set; }
    public ulong KernelLibraryIndex { get; set; }

    public List<DartClass> Classes { get; } = [];

    public static void InitPropertySetters(DartPropertySetters<DartLibrary> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.Name);
        setters.AddRef(e => e.Url);
        setters.AddRef(e => e.PrivateKey);
        setters.AddRef(e => e.Dictionary);
        setters.AddRef(e => e.Metadata);
        setters.AddRef(e => e.TopLevelClass);
        setters.AddRef(e => e.UsedScripts);
        setters.AddRef(e => e.LoadingUnit);
        setters.AddRef(e => e.Imports);
        setters.AddRef(e => e.Exports);

        if (kind != SnapshotKind.kFullAOT)
        {
            setters.AddRef(e => e.Dependencies);
            setters.AddRef(e => e.KernelProgramInfo);
        }

        setters.AddValue(e => e.Index);
        setters.AddValue(e => e.NumImports);
        setters.AddValue(e => e.LoadState);
        setters.AddValue(e => e.Flags);

        if (kind != SnapshotKind.kFullAOT)
        {
            setters.AddValue(e => e.KernelLibraryIndex);
        }
    }
}
