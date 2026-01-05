using DartParser.Dart.Objects.Singletons;
using Semver;
using System.Diagnostics;
using System.Reflection;

namespace DartParser.Dart.Objects.BaseTypes
{
    public interface IDartObject
    {
        static abstract DartObject Default();
    }

    [DebuggerDisplay("{Type} {Description}")]
    public class DartObject : IDartObject, IHasObjectLayout<DartObject>
    {
        public virtual string? Description { get; init; }
        public ClassId Type { get; set; }

        public List<(DartObject Link, string PropertyName)> LinkedObjects { get; } = [];

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

        public static DartNull Null => DartNull.Instance;

        public static DartObject Default() => Null;

        public static void InitObjectLayout(DartObjectLayout<DartObject> setters, SemVersion version, SnapshotKind kind, bool isProduct, bool is64Bit)
        {
        }
    }
}
