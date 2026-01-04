using DartParser.Dart;
using DartParser.Dart.Objects;
using Semver;

namespace DartParser.Dart.Objects.ToCheck
{
    public class DartInteger(ClassId cid) : DartObject(cid), IDartObject<DartInteger>
    {
        public DartInteger() : this(ClassId.kIntegerCid) { }

        public long Value { get; set; }

        public static void InitPropertySetters(DartPropertySetters<DartInteger> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddValue(e => e.Value);
        }
    }
}
