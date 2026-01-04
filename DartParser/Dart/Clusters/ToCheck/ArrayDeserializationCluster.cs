using DartParser.Dart;
using DartParser.Dart.Clusters;
using DartParser.Dart.Objects;
using DartParser.Dart.Objects.ToCheck;
using System.Diagnostics;

namespace DartParser.Dart.Clusters.ToCheck;

public class ArrayDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterBase(isCanonical, isImmutable, cid, isRootUnit, version)
{
    private DartArray[] Entries = [];

    public override void ReadAllocate(Snapshot snapshot)
    {
        var count = snapshot.ReadUnsigned();

        Entries = new DartArray[count];

        for (var i = 0UL; i < count; i++)
        {
            var length = snapshot.ReadUnsigned();

            var entry = new DartArray(ClassId.kImmutableArrayCid)
            {
                Data = [.. Enumerable.Repeat(DartObject.Null, (int)length)]
            };

            Entries[i] = entry;
            snapshot.Objects.Add(entry);
        }
    }

    public override void ReadFill(Snapshot snapshot)
    {
        foreach (var entry in Entries)
        {
            var length = snapshot.ReadUnsigned();
            Debug.Assert(length <= (ulong)entry.Data.Count);
            entry.Length = length;
            entry.TypeArguments = snapshot.ReadRef<DartTypeArguments>();

            for (var i = 0UL; i < length; i++)
            {
                entry.Data[(int)i] = snapshot.ReadRef<DartObject>();
            }
        }
    }
}
