using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.VariableLength;

public class DartExceptionHandlers() : DartObject(ClassId.kExceptionHandlersCid), IHasData<DartExceptionHandlers.ExceptionHandlerInfo>, IHasPropertySetters<DartExceptionHandlers>
{
    public record struct ExceptionHandlerInfo : IHasPropertySetters<ExceptionHandlerInfo>
    {
        public uint HandlerPCOffset { get; set; }
        public ushort OuterTryIndex { get; set; }
        public byte NeedsStackTrace { get; set; }
        public byte HasCatchAll { get; set; }
        public byte IsGenerated { get; set; }

        public static void InitPropertySetters(DartPropertySetters<ExceptionHandlerInfo> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddValue(e => e.HandlerPCOffset);
            setters.AddValue(e => e.OuterTryIndex);
            setters.AddValue(e => e.NeedsStackTrace);
            setters.AddValue(e => e.HasCatchAll);
            setters.AddValue(e => e.IsGenerated);
        }
    }

    public ExceptionHandlerInfo[] Data { get; set; } = [];
    public ulong PackedFields { get; set; }
    public DartArray? HandledTypesData { get; set; }

    ulong IHasData<ExceptionHandlerInfo>.Length => PackedFields >> 1;
    public bool IsAsync => (PackedFields & 1) != 0;

    public static void InitPropertySetters(DartPropertySetters<DartExceptionHandlers> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddUnsigned(e => e.PackedFields);
        setters.AddRef(e => e.HandledTypesData);
    }
}
