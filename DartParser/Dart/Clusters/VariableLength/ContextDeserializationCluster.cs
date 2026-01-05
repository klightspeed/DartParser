using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.VariableLength;

namespace DartParser.Dart.Clusters.VariableLength;

public class ContextDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterVariableLengthRef<DartContext, DartObject>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
