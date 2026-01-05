using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.Other;
using System.Diagnostics;

namespace DartParser.Dart.Clusters.Other;

public class CodeDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterFixedSize<DartCode>(isCanonical, isImmutable, cid, isRootUnit, version)
{
    protected DartCode[] DeferredEntries = [];

    public override void ReadAllocate(Snapshot snapshot)
    {
        var count = snapshot.ReadUnsigned();
        Entries = new DartCode[count];

        for (var i = 0UL; i < count; i++)
        {
            var entry = ReadAllocateOne(snapshot, false);
            Entries[i] = entry;
            snapshot.AddObject(entry);
        }

        count = snapshot.ReadUnsigned();
        DeferredEntries = new DartCode[count];

        for (var i = 0UL; i < count; i++)
        {
            var entry = ReadAllocateOne(snapshot, true);
            Entries[i] = entry;
            snapshot.AddObject(entry);
        }
    }

    private DartCode ReadAllocateOne(Snapshot snapshot, bool deferred)
    {
        var stateBits = snapshot.Read<ulong>();

        return new DartCode
        {
            StateBits = stateBits,
            Deferred = deferred
        };
    }

    public override void ReadFill(Snapshot snapshot)
    {
        ReadFill(snapshot, Entries, false);

        if (snapshot.Kind == SnapshotKind.kFullAOT)
        {
            ReadFill(snapshot, DeferredEntries, true);
        }
        else
        {
            Debug.Assert(DeferredEntries.Length == 0);
        }
    }

    private void ReadFill(Snapshot snapshot, DartCode[] entries, bool deferred)
    {
        foreach (var entry in entries)
        {
            entry.ReadInstructions(snapshot);
            snapshot.FillFields(entry);
        }
    }
}
