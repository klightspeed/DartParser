using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Clusters.BaseTypes;

public class DeserializationClusterFixedSize<T>(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, SemVersion version)
    : DeserializationClusterBase(isCanonical, isImmutable, cid, isRootUnit, version)
    where T : DartObject, IHasPropertySetters<T>, new()
{
    protected T[] Entries { get; set; } = [];

    public override void ReadAllocate(Snapshot snapshot)
        => Entries = ReadAllocateFixedSize<T>(snapshot, ClassId);

    public override void ReadFill(Snapshot snapshot)
        => ReadFillFixedSize(snapshot, Entries);
}
