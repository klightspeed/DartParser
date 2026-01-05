using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.VariableLength;
using Semver;

namespace DartParser.Dart.Objects.Canonical;

public class DartTypeArguments() : DartInstance(ClassId.kTypeArgumentsCid), IHasData<DartAbstractType>, IHasPropertySetters<DartTypeArguments>
{
    public ulong Count { get; set; }
    public ulong Hash { get; set; }
    public ulong Nullability { get; set; }
    public DartArray? Instantiations { get; set; }
    public DartAbstractType?[] Types { get; set; } = [];
    DartAbstractType?[] IHasData<DartAbstractType>.Data { get => Types; set => Types = value; }
    ulong IHasData<DartAbstractType>.Length => Count;

    public static void InitPropertySetters(DartPropertySetters<DartTypeArguments> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddUnsigned(e => e.Count);
        setters.AddValue(e => e.Hash);
        setters.AddUnsigned(e => e.Nullability);
        setters.AddRef(e => e.Instantiations);
    }
}
