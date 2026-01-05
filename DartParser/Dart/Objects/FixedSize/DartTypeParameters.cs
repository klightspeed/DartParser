using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using DartParser.Dart.Objects.VariableLength;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartTypeParameters() : DartObject(ClassId.kTypeParametersCid), IHasPropertySetters<DartTypeParameters>
{
    public DartArray? Names { get; set; }
    public DartArray? Flags { get; set; }
    public DartTypeArguments? Bounds { get; set; }
    public DartTypeArguments? Defaults { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartTypeParameters> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.Names);
        setters.AddRef(e => e.Flags);
        setters.AddRef(e => e.Bounds);
        setters.AddRef(e => e.Defaults);
    }
}
