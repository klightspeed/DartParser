using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Clusters.BaseTypes;

public class DeserializationClusterVariableLengthWithFillNew<T, U>(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, SemVersion version)
    : DeserializationClusterFixedSize<T>(isCanonical, isImmutable, cid, isRootUnit, version)
    where T : DartObject, IHasPropertySetters<T>, IHasData<U>, new()
    where U : IHasPropertySetters<U>, new()
{
    public override void ReadAllocate(Snapshot snapshot)
        => Entries = ReadAllocateVariableLength<T, U>(snapshot, ClassId);

    public override void ReadFill(Snapshot snapshot)
        => ReadFillVariableLengthWithFillNew<T, U>(snapshot, Entries);
}
