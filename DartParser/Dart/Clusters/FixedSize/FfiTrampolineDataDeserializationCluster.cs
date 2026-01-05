using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.FixedSize;

namespace DartParser.Dart.Clusters.FixedSize;

public class FfiTrampolineDataDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterFixedSize<DartFfiTrampolineData>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
