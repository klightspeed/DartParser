namespace DartParser.Dart.Objects.BaseTypes;

public interface IHasData<T>
{
    ulong Length { get; }
    T?[] Data { get; set; }
}
