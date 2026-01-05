using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using DartParser.Dart.Objects.VariableLength;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartGrowableObjectArray() : DartInstance(ClassId.kGrowableObjectArrayCid), IHasPropertySetters<DartGrowableObjectArray>
{
    public DartTypeArguments? TypeArguments { get; set; }
    public DartInteger? Length { get; set; }
    public DartArray? Data { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartGrowableObjectArray> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.TypeArguments);
        setters.AddRef(e => e.Length);
        setters.AddRef(e => e.Data);
    }
}
