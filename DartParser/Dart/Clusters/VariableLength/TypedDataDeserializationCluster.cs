using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.VariableLength;

namespace DartParser.Dart.Clusters.VariableLength;

public class TypedDataDeserializationCluster<T>(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterVariableLengthRaw<DartTypedDataArray<T>, T>(isCanonical, isImmutable, cid, isRootUnit, version)
    where T : struct
{
}
