using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using DartParser.Dart.Objects.VariableLength;
using Semver;

namespace DartParser.Dart.Objects.FixedSize
{
    public class DartRegExp() : DartInstance(ClassId.kRegExpCid), IHasPropertySetters<DartRegExp>
    {
        public DartArray? CaptureNameMap { get; set; }
        public DartString? Pattern { get; set; }
        public DartObject? OneByte { get; set; }
        public DartObject? TwoByte { get; set; }
        public DartObject? OneByteSticky { get; set; }
        public DartObject? TwoByteSticky { get; set; }
        public ulong NumOneByteRegisters { get; set; }
        public ulong NumTwoByteRegisters { get; set; }
        public byte TypeFlags { get; set; }

        public static void InitPropertySetters(DartPropertySetters<DartRegExp> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddRef(e => e.CaptureNameMap);
            setters.AddRef(e => e.Pattern);
            setters.AddRef(e => e.OneByte);
            setters.AddRef(e => e.TwoByte);
            setters.AddRef(e => e.OneByteSticky);
            setters.AddRef(e => e.TwoByteSticky);
            setters.AddValue(e => e.NumOneByteRegisters);
            setters.AddValue(e => e.NumTwoByteRegisters);
            setters.AddValue(e => e.TypeFlags);
        }
    }
}
