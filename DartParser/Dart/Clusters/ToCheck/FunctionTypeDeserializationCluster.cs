using DartParser.Dart;
using DartParser.Dart.Clusters;
using DartParser.Dart.Objects.ToCheck;

namespace DartParser.Dart.Clusters.ToCheck;

public class FunctionTypeDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : CanonicalSetDeserializationCluster<DartFunctionType>(isCanonical, isImmutable, cid, isRootUnit, version)
{
    public override void ReadAllocate(Snapshot snapshot)
    {
        var count = (int)snapshot.ReadUnsigned();
        Entries = new DartFunctionType[count];

        for (int i = 0; i < count; i++)
        {
            var entry = new DartFunctionType();
            snapshot.Objects.Add(entry);
            Entries[i] = entry;
        }

        BuildCanonicalSetFromLayout(snapshot);
    }

    public override void ReadFill(Snapshot snapshot)
    {
        foreach (var entry in Entries)
        {

        }
    }
}
