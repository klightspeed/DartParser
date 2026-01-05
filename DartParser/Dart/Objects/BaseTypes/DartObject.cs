using Semver;
using System.Reflection;

namespace DartParser.Dart.Objects.BaseTypes
{
    public interface IDartObject
    {
        static abstract DartObject Default();
    }

    public class DartObject : IDartObject, IHasObjectLayout<DartObject>
    {
        public virtual string? Description { get; init; }
        public ClassId Type { get; set; }

        public DartObject(ClassId type)
        {
            this.Description = type.ToString();
            this.Type = type;
        }

        public DartObject(string description, ClassId type)
        {
            this.Description = description;
            this.Type = type;
        }

        public static DartObject Null { get; } = new("null", ClassId.kNullCid);

        public static DartObject Default() => Null;

        public static void InitObjectLayout(DartObjectLayout<DartObject> setters, SemVersion version, SnapshotKind kind, bool isProduct, bool is64Bit)
        {
        }
    }
}
