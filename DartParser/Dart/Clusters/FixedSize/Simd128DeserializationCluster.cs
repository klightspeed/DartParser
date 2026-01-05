using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.FixedSize;
using System.Numerics;

namespace DartParser.Dart.Clusters.FixedSize;

public class Simd128DeserializationCluster<T>(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterFixedSize<DartSimd<T>>(isCanonical, isImmutable, cid, isRootUnit, version)
    where T : unmanaged, IBinaryNumber<T>
{
}
