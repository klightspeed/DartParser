using DartParser.Dart.Objects.BaseTypes;
using System.Runtime.InteropServices;

namespace DartParser
{
    public class DartIsolate(ClassTable classTable, bool isVmIsolate, bool isBigEndian, bool is64Bit, Architecture arch)
    {
        public bool IsVmIsolate { get; } = isVmIsolate;
        public bool Is64Bit { get; } = is64Bit;
        public bool IsBigEndian { get; } = isBigEndian;
        public Architecture Architecture { get; } = arch;
        public ClassTable ClassTable { get; } = classTable;
        public List<DartObject> Objects { get; } = [];

        public DartIsolate(DartIsolate vmIsolate)
            : this(new ClassTable(vmIsolate.ClassTable, vmIsolate.Objects), false, vmIsolate.IsBigEndian, vmIsolate.Is64Bit, vmIsolate.Architecture)
        {

        }
    }
}
