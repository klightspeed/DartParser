using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.Canonical;

namespace DartParser.Dart.Clusters.Canonical;

public class StringDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : CanonicalSetDeserializationCluster<DartString>(isCanonical, isImmutable, cid, isRootUnit, version)
{
    public override void ReadAllocate(Snapshot snapshot)
    {
        int count = (int)snapshot.ReadUnsigned();
        Entries = new DartString[count];

        for (int i = 0; i < count; i++)
        {
            var lengthAndCid = snapshot.ReadUnsigned();
            var length = lengthAndCid >> 1;
            var cid = (lengthAndCid & 1) == 0 ? ClassId.kOneByteStringCid : ClassId.kTwoByteStringCid;
            var entry = new DartString(cid, length);
            Entries[i] = entry;
            snapshot.Objects.Add(entry);
        }

        base.BuildCanonicalSetFromLayout(snapshot);
    }

    public override void ReadFill(Snapshot snapshot)
    {
        foreach (var entry in Entries)
        {
            entry.Fill(snapshot);
        }
    }
}
