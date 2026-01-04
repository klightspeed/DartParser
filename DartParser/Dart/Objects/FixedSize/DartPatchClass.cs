using DartParser.Dart;
using DartParser.Dart.Objects;
using DartParser.Dart.Objects.ToCheck;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartPatchClass() : DartObject(ClassId.kPatchClassCid), IDartObject<DartPatchClass>
{
    public DartClass? WrappedClass { get; set; }
    public DartScript? Script { get; set; }
    public DartKernelProgramInfo? KernelProgramInfo { get; set; }
    public ulong KernelLibraryIndex { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartPatchClass> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.WrappedClass);
        setters.AddRef(e => e.Script);

        if (kind != SnapshotKind.kFullAOT)
        {
            setters.AddRef(e => e.KernelProgramInfo);
            setters.AddValue(e => e.KernelLibraryIndex);
        }
    }
}
