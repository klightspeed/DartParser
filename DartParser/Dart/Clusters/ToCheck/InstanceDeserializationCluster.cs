using DartParser.Dart;
using DartParser.Dart.Clusters;
using DartParser.Dart.Objects;

namespace DartParser.Dart.Clusters.ToCheck;

public class InstanceDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterBase(isCanonical, isImmutable, cid, isRootUnit, version)
{
    private List<DartObject> PredefinedObjects = [];
    private List<DartObject> Objects = [];

    public override void ReadAllocate(Snapshot snapshot)
    {
        int count = (int)snapshot.ReadUnsigned();

        for (int i = 0; i < count; i++)
        {
            throw new NotImplementedException();
        }

        throw new NotImplementedException();
    }
}
