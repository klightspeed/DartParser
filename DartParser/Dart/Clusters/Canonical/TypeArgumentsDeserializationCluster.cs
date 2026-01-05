using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using DartParser.Dart.Objects.VariableLength;

namespace DartParser.Dart.Clusters.Canonical;

public class TypeArgumentsDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : CanonicalSetDeserializationCluster<DartTypeArguments>(isCanonical, isImmutable, cid, isRootUnit, version)
{
    public override void ReadAllocate(Snapshot snapshot)
    {
        int count = (int)snapshot.ReadUnsigned();
        Entries = new DartTypeArguments[count];

        for (int i = 0; i < count; i++)
        {
            var entry = new DartTypeArguments(snapshot.ReadUnsigned());
            Entries[i] = entry;
            snapshot.Objects.Add(entry);
        }

        base.BuildCanonicalSetFromLayout(snapshot);
    }

    public override void ReadFill(Snapshot snapshot)
    {
        foreach (var entry in Entries)
        {
            entry.Count2 = snapshot.ReadUnsigned();
            entry.Hash = snapshot.ReadUnsigned();
            entry.Nullability = snapshot.ReadUnsigned();
            entry.Instantiations = snapshot.ReadRef<DartArray>();
            
            for (ulong i = 0; i < entry.Count2; i++)
            {
                entry.Types.Add(snapshot.ReadRef<DartAbstractType>());
            }
        }
    }
}
