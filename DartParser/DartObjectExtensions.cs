using DartParser.Dart.Objects;
using DartParser.Dart.Objects.ToCheck;

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
