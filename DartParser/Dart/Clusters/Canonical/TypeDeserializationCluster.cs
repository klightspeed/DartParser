using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.Canonical;

namespace DartParser.Dart.Clusters.Canonical;

public class TypeDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : CanonicalSetDeserializationCluster<DartType>(isCanonical, isImmutable, cid, isRootUnit, version)
{
    public override void ReadAllocate(Snapshot snapshot)
    {
        Entries = ReadAllocateFixedSize<DartType>(snapshot, ClassId);
        BuildCanonicalSetFromLayout(snapshot);
    }

    public override void ReadFill(Snapshot snapshot)
    {
        ReadFillFixedSize(snapshot, Entries);
    }
}
