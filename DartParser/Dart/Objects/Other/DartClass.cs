using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using DartParser.Dart.Objects.FixedSize;
using DartParser.Dart.Objects.VariableLength;
using Semver;
using System.Diagnostics;

namespace DartParser.Dart.Objects.Other;

[DebuggerDisplay("{Type} {ClassId}")]
public class DartClass() : DartObject(ClassId.kClassCid), IHasPropertySetters<DartClass>
{
    public DartClass(ClassId cid) : this()
    {
        ClassId = cid;
    }

    public DartString? Name { get; set; }
    public DartString? UserName { get; set; }
    public DartArray? Functions { get; set; }
    public DartArray? FunctionsHashTable { get; set; }
    public DartArray? Fields { get; set; }
    public DartArray? FieldOffsets { get; set; }
    public DartArray? Interfaces { get; set; }
    public DartScript? Script { get; set; }
    public DartLibrary? Library { get; set; }
    public DartTypeParameters? TypeParameters { get; set; }
    public DartType? SuperType { get; set; }
    public DartArray? Constants { get; set; }
    public DartType? DeclarationType { get; set; }
    public DartArray? InvocationDispatcherCache { get; set; }
    public DartGrowableObjectArray? DirectImplementors { get; set; }
    public DartGrowableObjectArray? DirectSubClasses { get; set; }
    public DartTypeArguments? DeclarationInstanceTypeArguments { get; set; }
    public DartCode? AllocationStub { get; set; }
    public DartWeakArray? DependentCode { get; set; }
    public ClassId ClassId { get; set; }
    public ulong KernelOffset { get; set; }
    public ulong InstanceSizeInWords { get; set; }
    public ulong NextFieldOffsetInWords { get; set; }
    public ulong TypeArgumentsFieldOffsetInWords { get; set; }
    public ulong NumTypeArguments { get; set; }
    public ulong NumNativeFields { get; set; }
    public ulong TokenPosition { get; set; }
    public ulong EndTokenPosition { get; set; }
    public ClassId ImplementorCid { get; set; }
    public ulong StateBits { get; set; }        
    public ulong UnboxedFieldsBitmap { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartClass> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.Name);

        if (!isProduct) setters.AddRef(e => e.UserName);

        setters.AddRef(e => e.Functions);
        setters.AddRef(e => e.FunctionsHashTable);
        setters.AddRef(e => e.Fields);
        setters.AddRef(e => e.FieldOffsets);
        setters.AddRef(e => e.Script);
        setters.AddRef(e => e.Library);
        setters.AddRef(e => e.TypeParameters);
        setters.AddRef(e => e.SuperType);
        setters.AddRef(e => e.Constants);
        setters.AddRef(e => e.DeclarationType);
        setters.AddRef(e => e.InvocationDispatcherCache);

        if (kind != SnapshotKind.kFullAOT || !isProduct)
        {
            setters.AddRef(e => e.DirectImplementors);
            setters.AddRef(e => e.DirectSubClasses);

            if (kind != SnapshotKind.kFullAOT)
            {
                setters.AddRef(e => e.DeclarationInstanceTypeArguments);
                setters.AddRef(e => e.AllocationStub);

                if (kind != SnapshotKind.kFull && kind != SnapshotKind.kFullCore)
                {
                    setters.AddRef(e => e.DependentCode);
                }
            }
        }

        setters.AddCid(e => e.ClassId);
        setters.AddValue(e => e.KernelOffset);
        setters.AddValue(e => e.InstanceSizeInWords);
        setters.AddValue(e => e.NextFieldOffsetInWords);
        setters.AddValue(e => e.TypeArgumentsFieldOffsetInWords);
        setters.AddValue(e => e.NumTypeArguments);
        setters.AddValue(e => e.NumNativeFields);

        if (kind != SnapshotKind.kFullAOT)
        {
            setters.AddValue(e => e.TokenPosition);
            setters.AddValue(e => e.EndTokenPosition);
            setters.AddCid(e => e.ImplementorCid);
        }

        setters.AddValue(e => e.StateBits);
        setters.AddValueIf(e => e.UnboxedFieldsBitmap, e => e.ClassId < ClassId.kTopLevelCidOffset);
    }
}
