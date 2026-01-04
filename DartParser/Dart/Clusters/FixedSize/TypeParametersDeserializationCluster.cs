using DartParser.Dart;
using DartParser.Dart.Clusters;
using DartParser.Dart.Objects.FixedSize;

namespace DartParser.Dart.Clusters.FixedSize;

public class TypeParametersDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterBase<DartTypeParameters>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
