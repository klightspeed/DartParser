using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.FixedSize;

namespace DartParser.Dart.Clusters.FixedSize;

public class SetDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterFixedSize<DartSet>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
