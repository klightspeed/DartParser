namespace DartParser;

public readonly record struct AddsDels<T>(List<ValueAfter<T>> Adds, List<ValueAfter<T>> Dels) where T : notnull;
