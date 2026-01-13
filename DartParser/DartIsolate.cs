using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using DartParser.Dart.Objects.FixedSize;
using DartParser.Dart.Objects.Other;
using DartParser.Dart.Objects.VariableLength;
using System.Runtime.InteropServices;

namespace DartParser;

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
    public List<DartType> Types { get; } = [];
    public List<(ulong CodeIndex, DartFunction Function)> FunctionsByCodeIndex { get; } = [];
    public List<DartCode> CodeBlocks { get; } = [];
    public List<DartUserDefinedInstance> UserDefinedInstances { get; } = [];
    public List<DartLoadingUnit> LoadingUnits { get; } = [];
    public DartInstructionsSection? InstructionsSection { get; set; }
    public DartInstructionsTable? InstructionsTable { get; set; }
    public Dictionary<(Type Type, string Name), List<(DartObject From, DartObject To)>> Links { get; } = [];

    public DartIsolate(DartIsolate vmIsolate)
        : this(new ClassTable(vmIsolate.ClassTable, vmIsolate.Objects), false, vmIsolate.IsBigEndian, vmIsolate.Is64Bit, vmIsolate.Architecture)
    {
    }

    private static int FirstNonZero<T>(T l, T r, params Span<Func<T, IComparable?>> items)
    {
        if (ReferenceEquals(l, r))
            return 0;

        if (l is null)
            return -1;

        if (r is null)
            return 1;

        foreach (var func in items)
        {
            var lv = func(l);
            var rv = func(r);

            if (ReferenceEquals(lv, rv))
                continue;

            if (lv is null)
                return -1;

            if (rv is null)
                return 1;

            var comp = lv.CompareTo(rv);

            if (comp != 0)
                return comp;
        }

        return 0;
    }

    public void PopulateObjects(Snapshot snapshot)
    {
        this.Objects.AddRange(snapshot.Objects);
        this.InstructionsSection = snapshot.InstructionsSection;
        this.InstructionsTable = snapshot.InstructionsTable;

        foreach (var obj in Objects)
        {
            if (ClassTable.TryGetValue(obj.Type, out var objcls))
            {
                obj.Class = objcls;
            }

            switch (obj)
            {
                case DartLibrary lib:
                    this.Libraries.Add(lib);
                    lib.LoadingUnit?.Libraries.Add(lib);
                    break;
                case DartClass cls:
                    this.Classes.Add(cls);
                    cls.Library?.Classes.Add(cls);

                    if (cls.DeclarationType is DartType decltype)
                    {
                        decltype.DeclarationClasses.Add(cls);
                    }

                    break;
                case DartType type:
                    this.Types.Add(type);
                    break;
                case DartFunction func:
                    FunctionsByCodeIndex.Add((func.CodeIndex, func));

                    if (func.Code == null && func.CodeIndex > 0 && func.CodeIndex <= (ulong)InstructionsTable.Code.Count)
                    {
                        func.Code = InstructionsTable.Code[(int)func.CodeIndex - 1];
                    }

                    break;
                case DartScript script:
                    this.Scripts.Add(script);
                    break;
                case DartLoadingUnit loadingUnit:
                    this.LoadingUnits.Add(loadingUnit);
                    break;
                case DartCode code:
                    this.CodeBlocks.Add(code);

                    if (code.InlinedIdToFunction is DartArray inlines)
                    {
                        foreach (var inline in inlines.Data)
                        {
                            if (inline is DartFunction func)
                            {
                                func.InlinedInCode.Add(code);
                            }
                        }
                    }
                    break;
            }

            switch (obj)
            {
                case DartFunction func when func.Owner is DartClass cls:
                    cls.OwnedFunctions.Add(func);
                    break;
                case DartCode code when code.Owner is DartFunction func:
                    func.OwnedCode.Add(code);
                    break;
                case DartField fld when fld.Owner is DartClass cls:
                    cls.OwnedFields.Add(fld);
                    break;
            }

            foreach (var link in obj.LinkedObjects)
            {
                if (!Links.TryGetValue((obj.GetType(), link.PropertyName), out var links))
                {
                    Links[(obj.GetType(), link.PropertyName)] = links = [];
                }

                links.Add((obj, link.Link));
            }
        }

        this.Libraries.Sort((l, r) => FirstNonZero(l, r, v => v.Url?.Value));
        this.Scripts.Sort((l, r) => FirstNonZero(l, r, v => v.Url?.Value));
        this.Classes.Sort((l, r) => FirstNonZero(l, r, v => v.Library?.Url?.Value, v => v.Name?.Value));
        this.CodeBlocks.Sort((l, r) => FirstNonZero(l, r, v => v.EntryPoint));
        this.FunctionsByCodeIndex.Sort((l, r) => FirstNonZero(l, r, v => v.CodeIndex));

        foreach (var lib in this.Libraries)
        {
            lib.Classes.Sort((l, r) => FirstNonZero(l, r, v => v.Name?.Value));
        }

        foreach (var cls in this.Classes)
        {
            cls.OwnedFields.Sort((l, r) => FirstNonZero(l, r, v => v.Name?.Value));
            cls.OwnedFunctions.Sort((l, r) => FirstNonZero(l, r, v => v.Name?.Value));
        }
    }
}
