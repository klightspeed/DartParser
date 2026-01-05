using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.VariableLength;

public class DartContext() : DartObject(ClassId.kContextCid), IHasPropertySetters<DartContext>, IHasData<DartObject>
{
    public ulong NumVariables { get; set; }
    public DartContext? Parent { get; set; }
    public DartObject?[] Data { get; set; } = [];

    ulong IHasData<DartObject>.Length => NumVariables;

    public static void InitPropertySetters(DartPropertySetters<DartContext> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddUnsigned(e => e.NumVariables);
        setters.AddRef(e => e.Parent);
    }
}
