using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.FixedSize;
using DartParser.Dart.Objects.Other;
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
        public List<DartLibrary> Libraries { get; } = [];
        public List<DartScript> Scripts { get; } = [];
        public List<DartClass> Classes { get; } = [];

        public DartIsolate(DartIsolate vmIsolate)
            : this(new ClassTable(vmIsolate.ClassTable, vmIsolate.Objects), false, vmIsolate.IsBigEndian, vmIsolate.Is64Bit, vmIsolate.Architecture)
        {
        }

        public void PopulateObjects(Snapshot snapshot)
        {
            this.Objects.AddRange(snapshot.Objects);
            this.Libraries.AddRange(Objects.OfType<DartLibrary>());
            this.Scripts.AddRange(Objects.OfType<DartScript>());
            this.Classes.AddRange(Objects.OfType<DartClass>());
        }
    }
}
