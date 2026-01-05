using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.VariableLength;

namespace DartParser.Dart.Clusters.VariableLength;

public class PcDescriptorsDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterVariableLengthRaw<DartPcDescriptors, byte>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
