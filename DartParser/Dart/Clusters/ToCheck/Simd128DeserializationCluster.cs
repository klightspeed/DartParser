using DartParser.Dart;
using DartParser.Dart.Clusters;
using DartParser.Dart.Objects.ToCheck;

namespace DartParser.Dart.Clusters.ToCheck;

public class Simd128DeserializationCluster<T>(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterBase<DartSimd<T>>(isCanonical, isImmutable, cid, isRootUnit, version)
    where T : unmanaged
{
}
