using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Singletons;

namespace DartParser;

public static class DartObjectExtensions
{
    extension(DartObject? o) {
        public T? Cast<T>()
            where T : DartObject
        {
            if (o is null or DartNull)
            {
                return null;
            }

            return (T)o;
        }
    }
}
