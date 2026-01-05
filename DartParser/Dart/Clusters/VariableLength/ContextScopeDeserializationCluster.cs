using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.VariableLength;

namespace DartParser.Dart.Clusters.VariableLength;

public class ContextScopeDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterVariableLengthWithFillRef<DartContextScope, DartContextScope.VariableDesc>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
