using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.Canonical;

namespace DartParser.Dart.Clusters.Other;

public class ROStringDataDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : CanonicalSetDeserializationCluster<DartString>(isCanonical, isImmutable, cid, isRootUnit, version)
{
    public override void ReadAllocate(Snapshot snapshot)
    {
        var count = snapshot.ReadUnsigned();
        Entries = new DartString[count];
        var offset = 0UL;

        for (var i = 0UL; i < count; i++)
        {
            offset += snapshot.ReadUnsigned() << (snapshot.Is64Bit ? 4 : 3);
            Entries[i] = snapshot.GetObjectAt<DartString>(offset)!;
        }

        BuildCanonicalSetFromLayout(snapshot);
    }

    public override void ReadFill(Snapshot snapshot)
    {
    }
}
