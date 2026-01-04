using DartParser.Dart;
using Semver;
using System.Reflection;

namespace DartParser.Dart.Objects
{
    public interface IDartObject
    {
        static abstract DartObject Default();
    }

    public interface IDartObject<T>
        where T : DartObject, IDartObject<T>
    {
        static abstract void InitPropertySetters(DartPropertySetters<T> setters, SemVersion version, SnapshotKind kind, bool isProduct);
    }

    public class DartObject : IDartObject
    {
        public virtual string? Description { get; init; }
        public ClassId Type { get; init; }

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
    }
}
