using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartUnlinkedCall() : DartCallSiteData(ClassId.kUnlinkedCallCid), IHasPropertySetters<DartUnlinkedCall>
{
    public bool CanPatchToMonomorphic { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartUnlinkedCall> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.TargetName);
        setters.AddRef(e => e.ArgsDescriptor);
        setters.AddValue(e => e.CanPatchToMonomorphic);
    }
}
