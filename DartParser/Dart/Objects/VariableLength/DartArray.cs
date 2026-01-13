using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using Semver;

namespace DartParser.Dart.Objects.VariableLength;

public class DartArray(ClassId cid) : DartObject(cid), IHasData<DartObject>, IHasPropertySetters<DartArray>
{
    public DartArray() : this(ClassId.kArrayCid) { }

    public DartTypeArguments? TypeArguments { get; set; }
    public ulong Length { get; set; }
    public DartObject?[] Data { get; set; } = [];

    public static void InitPropertySetters(DartPropertySetters<DartArray> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddUnsigned(e => e.Length);
        setters.AddRef(e => e.TypeArguments);
    }
}
