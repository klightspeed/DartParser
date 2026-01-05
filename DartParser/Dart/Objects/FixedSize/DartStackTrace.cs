using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.VariableLength;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartStackTrace() : DartObject(ClassId.kStackTraceCid), IHasPropertySetters<DartStackTrace>
{
    public DartStackTrace? AsyncLink { get; set; }
    public DartArray? CodeArray { get; set; }
    public DartTypedData? PCOffsetArray { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartStackTrace> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.AsyncLink);
        setters.AddRef(e => e.CodeArray);
        setters.AddRef(e => e.PCOffsetArray);
    }
}
