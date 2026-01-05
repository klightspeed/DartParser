using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.FixedSize;

namespace DartParser.Dart.Clusters.FixedSize;

public class ExternalTypedDataDeserializationCluster<T>(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterFixedSize<DartExternalTypedData<T>>(isCanonical, isImmutable, cid, isRootUnit, version)
    where T : struct
{
    public override void ReadFill(Snapshot snapshot)
        => ReadFillVariableLengthRaw<DartExternalTypedData<T>, T>(snapshot, Entries);
}
