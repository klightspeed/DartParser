using DartParser.Dart;
using DartParser.Dart.Clusters;
using DartParser.Dart.Objects.FixedSize;

namespace DartParser.Dart.Clusters.FixedSize;

public class LanguageErrorDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterBase<DartLanguageError>(isCanonical, isImmutable, cid, isRootUnit, version)
{
}
