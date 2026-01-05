using Semver;

namespace DartParser.Dart.Objects.BaseTypes
{
    internal interface IHasObjectLayout<T>
    {
        static abstract void InitObjectLayout(DartObjectLayout<T> setters, SemVersion version, SnapshotKind kind, bool isProduct, bool is64Bit);
    }
}
