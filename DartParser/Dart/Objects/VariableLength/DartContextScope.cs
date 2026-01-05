using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using DartParser.Dart.Objects.FixedSize;
using Semver;

namespace DartParser.Dart.Objects.VariableLength;

public class DartContextScope() : DartObject(ClassId.kContextCid), IHasData<DartContextScope.VariableDesc>, IHasPropertySetters<DartContextScope>
{
    public record struct VariableDesc : IHasPropertySetters<VariableDesc>
    {
        public DartInteger? DeclarationTokenPosition { get; set; }
        public DartInteger? TokenPosition { get; set; }
        public DartString? Name { get; set; }
        public DartInteger? Flags { get; set; }
        public DartInteger? LateInitOffset { get; set; }
        public DartAbstractType? Type { get; set; }
        public DartInteger? Cid { get; set; }
        public DartInteger? ContextIndex { get; set; }
        public DartInteger? ContextLevel { get; set; }
        public DartInteger? KernelOffset { get; set; }

        public static void InitPropertySetters(DartPropertySetters<VariableDesc> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddRef(e => e.DeclarationTokenPosition);
            setters.AddRef(e => e.TokenPosition);
            setters.AddRef(e => e.Name);
            setters.AddRef(e => e.Flags);
            setters.AddRef(e => e.LateInitOffset);
            setters.AddRef(e => e.Type);
            setters.AddRef(e => e.Cid);
            setters.AddRef(e => e.ContextIndex);
            setters.AddRef(e => e.ContextLevel);
            setters.AddRef(e => e.KernelOffset);
        }
    }

    public ulong NumVariables { get; set; }
    public bool IsImplicit { get; set; }
    public VariableDesc[] Data { get; set; } = [];

    ulong IHasData<VariableDesc>.Length => NumVariables;

    public static void InitPropertySetters(DartPropertySetters<DartContextScope> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddUnsigned(e => e.NumVariables);
        setters.AddValue(e => e.IsImplicit);
    }
}
