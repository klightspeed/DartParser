using DartParser.Dart.Objects.ToCheck;
using System.Diagnostics.CodeAnalysis;

namespace DartParser.Comparers;

public class DartTypeComparer : IEqualityComparer<DartType>
{
    public static DartTypeComparer Instance { get; } = new();

    public bool Equals(DartType? x, DartType? y)
    {
        throw new NotImplementedException();
    }

    public int GetHashCode([DisallowNull] DartType obj)
    {
        throw new NotImplementedException();
    }
}
