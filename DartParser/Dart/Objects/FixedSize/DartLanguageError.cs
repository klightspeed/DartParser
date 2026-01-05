using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using Semver;

namespace DartParser.Dart.Objects.FixedSize
{
    public class DartLanguageError() : DartError(ClassId.kLanguageErrorCid), IHasPropertySetters<DartLanguageError>
    {
        public DartError? PreviousError { get; set; }
        public DartScript? Script { get; set; }
        public DartString? Message { get; set; }
        public DartString? FormattedMessage { get; set; }
        public ulong TokenPosition { get; set; }
        public bool ReportAfterToken { get; set; }
        public byte Kind { get; set; }

        public static void InitPropertySetters(DartPropertySetters<DartLanguageError> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddRef(e => e.PreviousError);
            setters.AddRef(e => e.Script);
            setters.AddRef(e => e.Message);
            setters.AddRef(e => e.FormattedMessage);
            setters.AddValue(e => e.TokenPosition);
            setters.AddValue(e => e.ReportAfterToken);
            setters.AddValue(e => e.Kind);
        }
    }
}
