using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.Other;

namespace DartParser.Dart.Clusters.Other;

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
