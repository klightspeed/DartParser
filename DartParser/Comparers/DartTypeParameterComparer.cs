using DartParser.Dart.Objects.ToCheck;
using System.Diagnostics.CodeAnalysis;

namespace DartParser.Comparers;

public class DartTypeParameterComparer : IEqualityComparer<DartTypeParameter>
{
    public static DartTypeParameterComparer Instance { get; } = new();

    public bool Equals(DartTypeParameter? x, DartTypeParameter? y)
    {
        throw new NotImplementedException();
    }

    public int GetHashCode([DisallowNull] DartTypeParameter obj)
    {
        throw new NotImplementedException();
    }
}
