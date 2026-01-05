using DartParser.Dart.Objects.BaseTypes;

namespace DartParser
{
    public abstract class DartPropertySetters
    {
        public static DartPropertySetters Empty { get; } = new DartPropertySetters<DartObject>();

        public abstract void FillFields<T>(T obj, Snapshot snapshot) where T : class;

        public abstract void FillFields<T>(ref T obj, Snapshot snapshot);
    }
}
