using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using DartParser.Dart.Objects.Other;
using DartParser.Dart.Objects.VariableLength;
using Semver;
using System.Diagnostics;

namespace DartParser.Dart.Objects.FixedSize;

[DebuggerDisplay("{Type} {Name?.Value}")]
public class DartFunction() : DartObject(ClassId.kFunctionCid), IHasPropertySetters<DartFunction>, IHasOwner
{
    public enum Kind
    {
        RegularFunction,
        ClosureFunction,
        ImplicitClosureFunction,
        GetterFunction,
        SetterFunction,
        Constructor,
        ImplicitGetter,
        ImplicitSetter,
        ImplicitStaticGetter,
        FieldInitializer,
        MethodExtractor,
        NoSuchMethodDispatcher,
        InvokeFieldDispatcher,
        IrregexpFunction,
        DynamicInvocationForwarder,
        FfiTrampoline,
        RecordFieldGetter
    }

    enum AsyncModifier
    {
        kNoModifier = 0x0,
        kAsyncBit = 0x1,
        kGeneratorBit = 0x2,
        kAsync = kAsyncBit,
        kSyncGen = kGeneratorBit,
        kAsyncGen = kAsyncBit | kGeneratorBit,
    }

    public DartString? Name { get; set; }
    public DartObject? Owner { get; set; }
    public DartFunctionType? Signature { get; set; }
    public DartObject? Data { get; set; }
    public DartCode? UnoptimizedCode { get; set; }
    public DartCode? Code { get; set; }
    public DartRefId ICDataArrayOrByteCode { get; set; }
    public DartArray? PositionalParameterNames { get; set; }
    public ulong CodeIndex { get; set; }
    public ulong TokenPosition { get; set; }
    public ulong EndTokenPosition { get; set; }
    public ulong KernelOffset { get; set; }
    public ulong IsOptimizable { get; set; }
    public ulong KindTag { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartFunction> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.Name);
        setters.AddRef(e => e.Owner);
        setters.AddRef(e => e.Signature);
        setters.AddRef(e => e.Data);

        if (kind == SnapshotKind.kFullAOT)
        {
            setters.AddUnsigned(e => e.CodeIndex);
        }
        else if (kind == SnapshotKind.kFullJIT)
        {
            setters.AddRef(e => e.UnoptimizedCode);
            setters.AddRef(e => e.Code);
            setters.AddRefId(e => e.ICDataArrayOrByteCode);
        }

        if (kind != SnapshotKind.kFullAOT)
        {
            setters.AddRef(e => e.PositionalParameterNames);
        }

        if (kind != SnapshotKind.kFullAOT || !isProduct)
        {
            setters.AddValue(e => e.TokenPosition);
        }

        if (kind != SnapshotKind.kFullAOT)
        {
            setters.AddValue(e => e.EndTokenPosition);
            setters.AddValue(e => e.KernelOffset);
            setters.AddValue(e => e.IsOptimizable);
        }

        setters.AddValue(e => e.KindTag);
    }
}
