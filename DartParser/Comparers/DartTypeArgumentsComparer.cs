using DartParser.Dart.Objects.Canonical;
using System.Diagnostics.CodeAnalysis;

namespace DartParser.Comparers;

public class DartTypeArgumentsComparer : IEqualityComparer<DartTypeArguments>
{
    public static DartTypeArgumentsComparer Instance { get; } = new();

    public bool Equals(DartTypeArguments? x, DartTypeArguments? y)
    {
        throw new NotImplementedException();
    }

    public int GetHashCode([DisallowNull] DartTypeArguments obj)
    {
        throw new NotImplementedException();
    }
}
