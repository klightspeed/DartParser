using DartParser.Dart;
using DartParser.Dart.Objects.BaseTypes;

namespace DartParser.Dart.Objects.ToCheck;

public class DartInstructionsSection() : DartObject(ClassId.kInstructionsSectionCid)
{
    public long PayloadLength { get; set; }
    public long BSSOffset { get; set; }
    public long InstructionsRelocatedAddress { get; set; }
    public long BuildIdOffset { get; set; }
    public ReadOnlyMemory<byte> Data { get; set; } = ReadOnlyMemory<byte>.Empty;
}
