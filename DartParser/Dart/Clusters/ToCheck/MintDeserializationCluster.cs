using DartParser;
using DartParser.Dart;
using DartParser.Dart.Objects.ToCheck;
using DartParser.Dart.Clusters;

namespace DartParser.Dart.Clusters.ToCheck;

public class MintDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterBase(isCanonical, isImmutable, cid, isRootUnit, version)
{
    public override void ReadAllocate(Snapshot snapshot)
    {
        var count = snapshot.ReadUnsigned();

        for (var i = 0UL; i < count; i++)
        {
            snapshot.Objects.Add(new DartMint { Value = snapshot.Read<long>() });
        }
    }

    public override void ReadFill(Snapshot snapshot)
    {
    }
}
