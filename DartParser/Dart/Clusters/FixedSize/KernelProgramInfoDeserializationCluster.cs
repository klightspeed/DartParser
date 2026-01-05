using DartParser.Dart;
using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.FixedSize;

namespace DartParser.Dart.Clusters.FixedSize;

public class KernelProgramInfoDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterFixedSize<DartKernelProgramInfo>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
