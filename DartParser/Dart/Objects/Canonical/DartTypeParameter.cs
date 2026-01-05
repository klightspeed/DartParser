using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.Canonical;

public class DartTypeParameter() : DartAbstractType(ClassId.kTypeParameterCid), IHasPropertySetters<DartTypeParameter>
{
    public DartObject? Owner { get; set; }
    public ushort Base { get; set; }
    public ushort Index { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartTypeParameter> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.TypeTestStub);
        setters.AddRef(e => e.Hash);
        setters.AddRef(e => e.Owner);
        setters.AddValue(e => e.Base);
        setters.AddValue(e => e.Index);
        setters.AddValue(e => e.Flags);
    }
}
