using DartParser.Dart;
using DartParser.Dart.Clusters;
using DartParser.Dart.Objects.ToCheck;

namespace DartParser.Dart.Clusters.ToCheck;

public class TypeDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : CanonicalSetDeserializationCluster<DartType>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
