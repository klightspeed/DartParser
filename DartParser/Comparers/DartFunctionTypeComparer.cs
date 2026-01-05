using DartParser.Dart.Objects.Canonical;
using System.Diagnostics.CodeAnalysis;

namespace DartParser.Comparers;

public class DartFunctionTypeComparer : IEqualityComparer<DartFunctionType>
{
    public static DartFunctionTypeComparer Instance { get; } = new();

    public bool Equals(DartFunctionType? x, DartFunctionType? y)
    {
        throw new NotImplementedException();
    }

    public int GetHashCode([DisallowNull] DartFunctionType obj)
    {
        throw new NotImplementedException();
    }
}
