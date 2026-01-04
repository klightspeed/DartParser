using DartParser.Dart;
using DartParser.Dart.Objects;
using DartParser.Dart.Objects.FixedSize;
using Semver;

namespace DartParser.Dart.Objects.ToCheck;

public class DartFunctionType() : DartAbstractType(ClassId.kFunctionTypeCid), IDartObject<DartFunctionType>
{
    public DartTypeParameters? TypeParameters { get; set; }
    public DartAbstractType? ResultType { get; set; }
    public DartArray? ParameterTypes { get; set; }
    public DartArray? NamedParameterNames { get; set; }

    public uint PackedParameterCounts { get; set; }
    public uint PacketTypeParameterCounts { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartFunctionType> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.TypeTestStub);
        setters.AddRef(e => e.Hash);
        setters.AddRef(e => e.TypeParameters);
        setters.AddRef(e => e.ResultType);
        setters.AddRef(e => e.ParameterTypes);
        setters.AddRef(e => e.NamedParameterNames);
        setters.AddValue(e => e.Flags);
        setters.AddValue(e => e.PackedParameterCounts);
        setters.AddValue(e => e.PacketTypeParameterCounts);
    }
}
