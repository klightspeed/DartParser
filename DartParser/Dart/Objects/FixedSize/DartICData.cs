using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.VariableLength;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartICData() : DartCallSiteData(ClassId.kICDataCid), IHasPropertySetters<DartICData>, IHasOwner
{
    public DartArray? Entries { get; set; }
    public DartAbstractType? ReceiversStaticType { get; set; }
    public DartObject? Owner { get; set; }
    public ulong DeoptId { get; set; }
    public ulong StateBits { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartICData> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.TargetName);
        setters.AddRef(e => e.ArgsDescriptor);
        setters.AddRef(e => e.Entries);

        if (kind != SnapshotKind.kFullAOT)
        {
            setters.AddRef(e => e.ReceiversStaticType);
            setters.AddRef(e => e.Owner);
            setters.AddValue(e => e.DeoptId);
        }

        setters.AddValue(e => e.StateBits);
    }
}
