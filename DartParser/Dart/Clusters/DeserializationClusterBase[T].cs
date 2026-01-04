using DartParser.Dart;
using DartParser.Dart.Objects;
using Semver;

namespace DartParser.Dart.Clusters;

public class DeserializationClusterBase<T>(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, SemVersion version)
    : DeserializationClusterBase(isCanonical, isImmutable, cid, isRootUnit, version)
    where T : DartObject, IDartObject<T>, new()
{
    protected T[] Entries { get; set; } = [];

    public override void ReadAllocate(Snapshot snapshot)
    {
        var count = (int)snapshot.ReadUnsigned();
        Entries = new T[count];

        for (int i = 0; i < count; i++)
        {
            var entry = new T { Type = this.ClassId };
            snapshot.Objects.Add(entry);
            Entries[i] = entry;
        }
    }

    public override void ReadFill(Snapshot snapshot)
    {
        foreach (var entry in Entries)
        {
            snapshot.FillFields(entry);
        }
    }
}
