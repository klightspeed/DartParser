using DartParser.Dart;
using DartParser.Dart.Objects;

namespace DartParser.Dart.Objects.ToCheck;

public class DartInstructionsSection() : DartObject(ClassId.kInstructionsSectionCid)
{
    public long PayloadLength { get; set; }
    public long BSSOffset { get; set; }
    public long InstructionsRelocatedAddress { get; set; }
    public long BuildIdOffset { get; set; }
    public Memory<byte> Data { get; set; } = Memory<byte>.Empty;
}
