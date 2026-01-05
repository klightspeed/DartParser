using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.Canonical;

public class DartType() : DartAbstractType(ClassId.kTypeCid), IHasPropertySetters<DartType>
{
    public DartTypeArguments? Arguments { get; set; }

    public ulong TypeFlags { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartType> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.TypeTestStub);
        setters.AddRef(e => e.Hash);
        setters.AddRef(e => e.Arguments);
        setters.AddUnsigned(e => e.Flags);
    }
}
