using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.FixedSize
{
    public class DartUnhandledException() : DartError(ClassId.kUnhandledExceptionCid), IHasPropertySetters<DartUnhandledException>
    {
        public DartInstance? Exception { get; set; }
        public DartInstance? StackTrace { get; set; }

        public static void InitPropertySetters(DartPropertySetters<DartUnhandledException> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddRef(e => e.Exception);
            setters.AddRef(e => e.StackTrace);
        }
    }
}
