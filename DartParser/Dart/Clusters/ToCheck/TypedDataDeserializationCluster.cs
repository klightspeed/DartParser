using DartParser.Dart;
using DartParser.Dart.Clusters;
using DartParser.Dart.Objects.ToCheck;

namespace DartParser.Dart.Clusters.ToCheck;

public class TypedDataDeserializationCluster<T>(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterBase<DartTypedDataBase<T>>(isCanonical, isImmutable, cid, isRootUnit, version)
    where T : struct
{
}
