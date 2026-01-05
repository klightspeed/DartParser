using DartParser.Dart;
using DartParser.Dart.Objects.BaseTypes;

namespace DartParser.Dart.Objects.ToCheck;

public class DartInstructions() : DartObject(ClassId.kInstructionsCid)
{
    public ulong SizeAndFlags { get; set; }

    public Memory<byte> Data { get; set; } = Memory<byte>.Empty;
}
