using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.VariableLength;

namespace DartParser.Dart.Clusters.VariableLength;

public class WeakArrayDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterVariableLengthRef<DartWeakArray, DartObject>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
