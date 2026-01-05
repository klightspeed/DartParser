using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using DartParser.Dart.Objects.VariableLength;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartScript() : DartObject(ClassId.kScriptCid), IHasPropertySetters<DartScript>
{
    public DartString? Url { get; set; }
    public DartString? ResolvedUrl { get; set; }
    public DartTypedData? LineStarts { get; set; }
    public DartObject? ConstantCoverage { get; set; }
    public DartArray? DebugPositions { get; set; }
    public DartKernelProgramInfo? KernelProgramInfo { get; set; }
    public DartString? Source { get; set; }

    public ulong LoadTimestamp { get; set; }
    public ulong KernelScriptIndex { get; set; }
    public ulong FlagsAndMaxPosition { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartScript> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.Url);

        if (kind != SnapshotKind.kFullAOT || !isProduct)
        {
            setters.AddRef(e => e.ResolvedUrl);
        }

        if (kind != SnapshotKind.kFullAOT)
        {
            setters.AddRef(e => e.LineStarts);

            if (!isProduct)
            {
                setters.AddRef(e => e.ConstantCoverage);
            }

            setters.AddRef(e => e.DebugPositions);
            setters.AddRef(e => e.KernelProgramInfo);
            setters.AddValue(e => e.FlagsAndMaxPosition);
        }

        setters.AddValue(e => e.KernelScriptIndex);
    }
}
