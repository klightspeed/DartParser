using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.BaseTypes;

namespace DartParser.Dart.Clusters.Other;

public class RODataDeserializationCluster<T>(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterBase(isCanonical, isImmutable, cid, isRootUnit, version)
    where T : DartObject
{
    public T[] Entries { get; set; } = [];

    public override void ReadAllocate(Snapshot snapshot)
    {
        var count = snapshot.ReadUnsigned();
        Entries = new T[count];
        var offset = 0UL;

        for (var i = 0UL; i < count; i++)
        {
            offset += snapshot.ReadUnsigned() << (snapshot.Is64Bit ? 4 : 3);
            Entries[i] = snapshot.GetObjectAt<T>(offset)!;
        }
    }

    public override void ReadFill(Snapshot snapshot)
    {
    }
}
