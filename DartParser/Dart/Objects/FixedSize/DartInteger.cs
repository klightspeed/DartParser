using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.FixedSize
{
    public class DartInteger(ClassId cid) : DartNumber(cid), IHasPropertySetters<DartInteger>
    {
        public DartInteger() : this(ClassId.kIntegerCid) { }

        public long Value { get; set; }

        public static void InitPropertySetters(DartPropertySetters<DartInteger> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddValue(e => e.Value);
        }
    }
}
