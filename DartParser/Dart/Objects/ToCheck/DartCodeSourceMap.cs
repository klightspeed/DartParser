using DartParser.Dart;
using DartParser.Dart.Objects;

namespace DartParser.Dart.Objects.ToCheck;

public class DartCodeSourceMap() : DartObject(ClassId.kCodeSourceMapCid)
{
    public Memory<byte> Payload { get; set; } = Memory<byte>.Empty;
}
