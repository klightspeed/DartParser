using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using Semver;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DartParser.Dart.Objects.Other;

public class DartInstructionsTable() : DartObject(ClassId.kInstructionsTableCid)
{
    public record struct DataEntry
    {
        public uint PCOffset { get; set; }
        public uint StackMapOffset { get; set; }
    }

    public record struct DataStruct
    {
        public uint CanonicalStackMapEntriesOffset { get; set; }
        public uint Length { get; set; }
        public uint FirstEntryWithCode { get; set; }
        public uint Padding { get; set; }
        public ReadOnlyMemory<DataEntry> Entries { get; set; }
    }

    public ulong Length { get; set; }
    public DataStruct ROData { get; set; }
    public ReadOnlyMemory<byte> Instructions { get; set; }

    public List<DartCode> Code { get; } = [];

    public void Init(Snapshot snapshot)
    {
        var rodata = new DataStruct { Entries = Memory<DataEntry>.Empty };

        if (snapshot.Kind == SnapshotKind.kFullAOT)
        {
            this.Length = snapshot.InstructionTableLength;
            this.Instructions = snapshot.SnapshotInstructions;
            ReadOnlyMemory<byte> data;

            if (snapshot.Version.ComparePrecedenceTo(SemVersion.Parse("3.0.0")) > 0)
            {
                var str = snapshot.GetObjectAt<DartString>(snapshot.InstructionTableDataOffset);
                Debug.Assert(str?.OneByteData?.Length > 0);
                data = str.OneByteData;
            }
            else
            {
                data = snapshot.DataImage;
            }

            var stream = new DartStream(data, snapshot.IsBigEndian, snapshot.Is64Bit);
            rodata.CanonicalStackMapEntriesOffset = stream.ReadRaw<uint>();
            rodata.Length = stream.ReadRaw<uint>();
            rodata.FirstEntryWithCode = stream.ReadRaw<uint>();
            rodata.Padding = stream.ReadRaw<uint>();
            var entries = new DataEntry[rodata.Length];
            rodata.Entries = entries;

            for (int i = 0; i < entries.Length; i++)
            {
                entries[i].PCOffset = stream.ReadRaw<uint>();
                entries[i].StackMapOffset = stream.ReadRaw<uint>();
            }
        }

        ROData = rodata;
        Code.EnsureCapacity((int)this.Length);
    }
}
