using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.VariableLength;

namespace DartParser.Dart.Clusters.VariableLength;

public class CompressedStackMapsDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterVariableLengthRaw<DartCompressedStackMaps, byte>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
