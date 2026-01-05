using DartParser.Dart.Objects.BaseTypes;

namespace DartParser.Dart.Objects.ToCheck;

public class DartInstructionsTable() : DartObject(ClassId.kInstructionsTableCid)
{
    public struct DataEntry
    {
        public uint PCOffset { get; set; }
        public uint StackMapOffset { get; set; }
    }

    public struct DataStruct
    {
        public uint CanonicalStackMapEntriesOffset { get; set; }
        public uint Length { get; set; }
        public uint FirstEntryWithCode { get; set; }
        public uint Padding { get; set; }
        public Memory<DataEntry> Entries { get; set; }
    }

    public ulong Length { get; set; }
    public DataStruct[] ROData { get; set; } = [];
    public ulong StartPC { get; set; }
    public ulong EndPC { get; set; }

    public void Init(int length, Snapshot snapshot)
    {
        ROData = [.. Enumerable.Repeat(new DataStruct { Entries = Memory<DataEntry>.Empty }, length) ];

        if (snapshot.Kind == SnapshotKind.kFullAOT)
        {
        }
    }
}
