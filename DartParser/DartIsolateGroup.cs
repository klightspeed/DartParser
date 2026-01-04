using ELFSharp.ELF.Sections;
using System.Runtime.InteropServices;
using ELF = ELFSharp.ELF;

namespace DartParser
{
    public class DartIsolateGroup(bool isBigEndian, bool is64Bit, Architecture arch)
    {
        public DartIsolate VMIsolate { get; } = new DartIsolate([], true, isBigEndian, is64Bit, arch);
        public Snapshot? VMSnapshot { get; private set; }
        public Snapshot? IsolateSnapshot { get; private set; }
        public List<DartIsolate> Isolates { get; } = [];
        public byte[] VMSnapshotData { get; private set; } = [];
        public byte[] VMSnapshotInstructions { get; private set; } = [];
        public byte[] IsolateSnapshotData { get; private set; } = [];
        public byte[] IsolateSnapshotInstructions { get; private set; } = [];

        public static bool TryProcessSnapshotFile(string filename, out DartIsolateGroup? isolateGroup)
        {
            isolateGroup = null;

            if (ELF.ELFReader.TryLoad(filename, out var elf))
            {
                return TryProcessSnapshotElf(elf, out isolateGroup);
            }

            return false;
        }

        public static bool TryProcessSnapshotElf(ELF.IELF elf, out DartIsolateGroup? isolateGroup)
        {
            var isBigEndian = elf.Endianess == ELFSharp.Endianess.BigEndian;
            var is64Bit = elf.Class == ELF.Class.Bit64;

            var arch = (elf.Machine, elf.Class) switch
            {
                (ELF.Machine.ARM, ELF.Class.Bit32) => Architecture.Arm,
                (ELF.Machine.AArch64, ELF.Class.Bit64) => Architecture.Arm64,
                ((ELF.Machine)0xF3, ELF.Class.Bit64) => Architecture.RiscV64,
                (ELF.Machine.Intel386, ELF.Class.Bit32) => Architecture.X86,
                (ELF.Machine.AMD64, ELF.Class.Bit64) => Architecture.X64,
                _ => throw new NotSupportedException()
            };

            isolateGroup = new DartIsolateGroup(isBigEndian, is64Bit, arch);

            if (elf.TryGetSection(".dynsym", out var dynsym) && dynsym is ISymbolTable symtable)
            {
                foreach (var sym in symtable.Entries)
                {
                    if (sym.PointedSection is not IProgBitsSection pointedSection)
                        continue;

                    ulong loadAddress = pointedSection switch
                    {
                        ProgBitsSection<uint> progBitsSection => progBitsSection.LoadAddress,
                        ProgBitsSection<ulong> progBitsSection => progBitsSection.LoadAddress,
                        _ => 0
                    };

                    (ulong offset, ulong size) = sym switch
                    {
                        SymbolEntry<uint> symEntry => (symEntry.Value, symEntry.Size),
                        SymbolEntry<ulong> symEntry => (symEntry.Value, symEntry.Size),
                        _ => (0UL, 0UL)
                    };

                    if (offset < loadAddress || size == 0)
                        continue;

                    offset -= loadAddress;

                    var sectionData = pointedSection.GetContents();
                    var symbolData = sectionData.AsSpan().Slice((int)offset, (int)size).ToArray();

                    switch (sym.Name)
                    {
                        case "kDartVmSnapshotData":
                        case "_kDartVmSnapshotData":
                            isolateGroup.VMSnapshotData = symbolData;
                            break;
                        case "kDartVmSnapshotInstructions":
                        case "_kDartVmSnapshotInstructions":
                            isolateGroup.VMSnapshotInstructions = symbolData;
                            break;
                        case "kDartIsolateSnapshotData":
                        case "_kDartIsolateSnapshotData":
                            isolateGroup.IsolateSnapshotData = symbolData;
                            break;
                        case "kDartIsolateSnapshotInstructions":
                        case "_kDartIsolateSnapshotInstructions":
                            isolateGroup.IsolateSnapshotInstructions = symbolData;
                            break;
                    }
                }
            }

            if (isolateGroup.VMSnapshotData.Length == 0
                || isolateGroup.VMSnapshotInstructions.Length == 0
                || isolateGroup.IsolateSnapshotData.Length == 0
                || isolateGroup.IsolateSnapshotInstructions.Length == 0)
            {
                return false;
            }

            return isolateGroup.TryProcessSnapshots();
        }

        public bool TryProcessSnapshots()
        {
            VMSnapshot = new Snapshot(VMIsolate, VMSnapshotData, VMSnapshotInstructions);

            if (!VMSnapshot.ProcessSnapshot())
            {
                return false;
            }

            var isolate = new DartIsolate(VMIsolate);
            Isolates.Add(isolate);
            IsolateSnapshot = new Snapshot(isolate, IsolateSnapshotData, IsolateSnapshotInstructions);
            return IsolateSnapshot.ProcessSnapshot();
        }
    }
}
