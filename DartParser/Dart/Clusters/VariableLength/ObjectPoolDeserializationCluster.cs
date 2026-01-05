using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.VariableLength;

namespace DartParser.Dart.Clusters.VariableLength;

public class ObjectPoolDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterVariableLengthWithFillRef<DartObjectPool, DartObjectPool.Entry>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
