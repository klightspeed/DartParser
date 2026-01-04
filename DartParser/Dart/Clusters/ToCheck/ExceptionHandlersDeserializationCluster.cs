using DartParser.Dart;
using DartParser.Dart.Clusters;

namespace DartParser.Dart.Clusters.ToCheck;

public class ExceptionHandlersDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterBase(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
