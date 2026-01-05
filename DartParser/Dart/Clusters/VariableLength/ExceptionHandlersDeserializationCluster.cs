using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.VariableLength;

namespace DartParser.Dart.Clusters.VariableLength;

public class ExceptionHandlersDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterVariableLengthWithFillRef<DartExceptionHandlers, DartExceptionHandlers.ExceptionHandlerInfo>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
