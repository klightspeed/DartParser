namespace DartParser;

public readonly record struct ValueAfter<T>(T Value, T After) where T : notnull;
