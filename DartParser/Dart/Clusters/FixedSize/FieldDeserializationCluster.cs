using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.FixedSize;

namespace DartParser.Dart.Clusters.FixedSize;

public class FieldDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterFixedSize<DartField>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
