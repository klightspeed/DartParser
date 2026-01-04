using DartParser.Dart;
using DartParser.Dart.Clusters;
using DartParser.Dart.Objects.FixedSize;

namespace DartParser.Dart.Clusters.FixedSize;

public class FunctionDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterBase<DartFunction>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
