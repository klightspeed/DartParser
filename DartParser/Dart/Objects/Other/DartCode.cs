using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.ToCheck;
using DartParser.Dart.Objects.VariableLength;
using Semver;
using System.Runtime.InteropServices;

namespace DartParser.Dart.Objects.Other;

public class DartCode() : DartObject(ClassId.kCodeCid), IHasPropertySetters<DartCode>
{
    public DartObjectPool? ObjectPool { get; set; }
    public DartInstructions? Instructions { get; set; }
    public DartObject? Owner { get; set; }
    public DartExceptionHandlers? ExceptionHandlers { get; set; }
    public DartPcDescriptors? PCDescriptors { get; set; }
    public DartObject? CatchEntry { get; set; }
    public DartCompressedStackMaps? CompressedStackMaps { get; set; }
    public DartArray? InlinedIdToFunction { get; set; }
    public DartCodeSourceMap? CodeSourceMap { get; set; }
    public DartInstructions? ActiveInstructions { get; set; }
    public DartArray? DeoptInfoArray { get; set; }
    public DartArray? StaticCallsTargetTable { get; set; }
    public DartObject? ReturnAddressMetadata { get; set; }
    public DartLocalVarDescriptors? VarDescriptors { get; set; }
    public DartArray? Comments { get; set; }

    public bool Deferred { get; set; }
    public bool HasMonomorphicEntryPoint { get; set; }
    public ulong EntryPoint { get; set; }
    public ulong UncheckedEntryPoint { get; set; }
    public ulong MonomorphicEntryPoint { get; set; }
    public ulong MonomorphicUncheckedEntryPoint { get; set; }
    public ulong CompileTimestamp { get; set; }
    public ulong StateBits { get; set; }
    public ulong InstructionsOffset { get; set; }
    public ulong UncheckedOffset { get; set; }
    public ulong ActiveOffset { get; set; }
    public ulong ActiveUncheckedOffset { get; set; }
    public ulong InstructionsLength { get; set; }
    public List<uint> Data { get; set; } = [];

    public bool Optimized => (StateBits & 1) != 0;
    public bool ForceOptimized => (StateBits & 2) != 0;
    public bool Alive => (StateBits & 4) != 0;
    public bool Discarded => (StateBits & 8) != 0;
    public ulong PtrOff => StateBits >> 5;

    public static void InitPropertySetters(DartPropertySetters<DartCode> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        if (kind == SnapshotKind.kFullJIT)
            setters.AddRef(e => e.ObjectPool);
        
        setters.AddRef(e => e.Owner);
        setters.AddRef(e => e.ExceptionHandlers);
        setters.AddRef(e => e.PCDescriptors);
        setters.AddRef(e => e.CatchEntry);
        
        if (kind == SnapshotKind.kFullJIT)
            setters.AddRef(e => e.CompressedStackMaps);

        setters.AddRef(e => e.InlinedIdToFunction);
        setters.AddRef(e => e.CodeSourceMap);

        if (kind == SnapshotKind.kFullJIT)
        {
            setters.AddRef(e => e.DeoptInfoArray);
            setters.AddRef(e => e.StaticCallsTargetTable);
        }

        if (!isProduct)
        {
            setters.AddRef(e => e.ReturnAddressMetadata);
            setters.AddRef(e => e.VarDescriptors);
            setters.AddRef(e => e.Comments);
        }
    }

    public void ReadInstructions(Snapshot snapshot)
    {
        if (Deferred)
        {
            return;
        }

        var (monoOffset, polyOffset) = (snapshot.Architecture, snapshot.Is64Bit, snapshot.Kind) switch
        {
            (Architecture.X64, true, SnapshotKind.kFullAOT) => (8, 22),
            (Architecture.X86, false, SnapshotKind.kFullAOT) => (0, 0),
            (Architecture.Arm, false, SnapshotKind.kFullAOT) => (0, 16),
            (Architecture.Arm64, true, SnapshotKind.kFullAOT) => (8, 24),
            (Architecture.RiscV64, true, SnapshotKind.kFullAOT) => (6, 18),
            (Architecture.X64, true, SnapshotKind.kFullJIT) => (8, 42),
            (Architecture.X86, false, SnapshotKind.kFullJIT) => (6, 36),
            (Architecture.Arm, false, SnapshotKind.kFullJIT) => (0, 44),
            (Architecture.Arm64, true, SnapshotKind.kFullJIT) => (8, 52),
            (Architecture.RiscV64, true, SnapshotKind.kFullJIT) => (6, 44),
            _ => (0, 0)
        };

        if (snapshot.Kind == SnapshotKind.kFullAOT)
        {
            var payloadInfo = snapshot.ReadUnsigned();
            var uncheckedOffset = payloadInfo >> 1;
            var hasMonomorphicEntryPoint = (payloadInfo & 1) != 0;
            this.UncheckedOffset = uncheckedOffset;
            this.HasMonomorphicEntryPoint = hasMonomorphicEntryPoint;
        }
        else if (snapshot.Kind == SnapshotKind.kFullJIT)
        {
            var instructionsOffset = snapshot.Read<uint>();
            var uncheckedOffset = snapshot.ReadUnsigned();
            var activeOffset = snapshot.Read<uint>();
            var activeUncheckedOffset = snapshot.ReadUnsigned();
            InstructionsOffset = instructionsOffset;
            UncheckedOffset = uncheckedOffset;
            ActiveOffset = activeOffset;
            ActiveUncheckedOffset = activeUncheckedOffset;
        }
    }
}
