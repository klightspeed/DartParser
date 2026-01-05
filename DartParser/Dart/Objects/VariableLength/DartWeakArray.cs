using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.VariableLength;

public class DartWeakArray() : DartObject(ClassId.kWeakArrayCid), IHasData<DartObject>, IHasPropertySetters<DartWeakArray>
{
    public ulong Length { get; set; }
    public DartObject?[] Data { get; set; } = [];

    public static void InitPropertySetters(DartPropertySetters<DartWeakArray> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddUnsigned(e => e.Length);
    }
}
