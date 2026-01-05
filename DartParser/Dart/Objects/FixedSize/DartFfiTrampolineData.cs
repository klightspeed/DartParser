using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartFfiTrampolineData() : DartObject(ClassId.kFfiTrampolineDataCid), IHasPropertySetters<DartFfiTrampolineData>
{
    public DartType? SignatureType { get; set; }
    public DartFunctionType? CSignature { get; set; }
    public DartFunction? CallbackTarget { get; set; }
    public DartInstance? CallbackExceptionalReturn { get; set; }
    public ulong CallbackId { get; set; }
    public ulong FfiFunctionKind { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartFfiTrampolineData> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.SignatureType);
        setters.AddRef(e => e.CSignature);
        setters.AddRef(e => e.CallbackTarget);
        setters.AddRef(e => e.CallbackExceptionalReturn);
        setters.AddValue(e => e.CallbackId);
        setters.AddValue(e => e.FfiFunctionKind);
    }
}
