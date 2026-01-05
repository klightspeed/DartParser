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
        Entries = ReadAllocateVariableLength<DartTypeArguments, DartAbstractType>(snapshot, ClassId);
        base.BuildCanonicalSetFromLayout(snapshot);
    }

    public override void ReadFill(Snapshot snapshot)
        => ReadFillVariableLengthRef<DartTypeArguments, DartAbstractType>(snapshot, Entries);
}
