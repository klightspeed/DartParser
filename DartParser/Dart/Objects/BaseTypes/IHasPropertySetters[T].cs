using Semver;

namespace DartParser.Dart.Objects.BaseTypes
{
    public interface IHasPropertySetters<T>
        where T : IHasPropertySetters<T>
    {
        static abstract void InitPropertySetters(DartPropertySetters<T> setters, SemVersion version, SnapshotKind kind, bool isProduct);
    }
}
