namespace DartParser.Dart;

public record struct ObjectClassIdTag
{
    public ulong RawTag { get; set; }
    public ObjectTagFlags Flags { get; set; }
    public uint Size { get; set; }
    public ClassId ClassId { get; set; }
    public uint Hash { get; set; }
}
