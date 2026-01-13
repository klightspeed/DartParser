using DartParser.Dart;
using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using DartParser.Dart.Objects.Other;
using DartParser.Dart.Objects.Singletons;
using DartParser.Dart.Objects.ToCheck;
using DartParser.Dart.Objects.VariableLength;
using Semver;
using System.Collections.Immutable;
using System.Diagnostics;

namespace DartParser;

public class ClassTable : Dictionary<ClassId, DartClass>
{
    private string? SnapshotVersion;
    private SemVersion? Version;
    private bool Is64Bit;
    private List<ClassId> ClassIdLookup = [];
    private List<DartObject> BaseObjects = [];

    public const ClassId kFirstInternalOnlyCid = ClassId.kClassCid;
    public const ClassId kLastInternalOnlyCid = ClassId.kUnwindErrorCid;

    public const ClassId kFirstErrorCid = ClassId.kErrorCid;
    public const ClassId kLastErrorCid = ClassId.kUnwindErrorCid;

    public const ClassId kFirstTypedDataCid = ClassId.kTypedDataInt8ArrayCid;
    public const ClassId kLastTypedDataCid = ClassId.kUnmodifiableTypedDataFloat64x2ArrayViewCid;

    public const ClassId kFirstFfiNumericCid = ClassId.kFfiInt8Cid;
    public const ClassId kLastffiNumericCid = ClassId.kFfiDoubleCid;

    public const ClassId kFirstFfiTypeCid = ClassId.kFfiInt8Cid;
    public const ClassId kLastFfiTypeCid = ClassId.kFfiBoolCid;

    public const ClassId kFirstFfiCid = ClassId.kFfiNativeFunctionCid;
    public const ClassId kLastFfiCid = ClassId.kFfiStructCid;

    public const ClassId kDeltaEncodedTypedDataCid = ClassId.kNativePointer;

    #region Objects
    public DartNull Null { get; } = new();

    public DartSentinel Sentinel { get; }
        = new() { Description = "sentinel" };

    public DartSentinel TransitionSentinel { get; }
        = new() { Description = "transition_sentinel" };

    public DartSentinel OptimizedOut { get; }
        = new() { Description = "<optimized_out>" };

    public DartArray EmptyArray { get; }
        = new(ClassId.kArrayCid) { Description = "<empty_array>" };

    public DartArray ZeroArray { get; }
        = new(ClassId.kArrayCid) { Description = "<zero_array>" };

    public DartArray EmptyInstantiationsCacheArray { get; }
        = new(ClassId.kArrayCid) { Description = "<empty_instantiations_cache_array>" };

    public DartArray EmptySubtypeTestCacheArray { get; }
        = new(ClassId.kArrayCid) { Description = "<empty_subtype_test_cache_array>" };

    public DartAbstractType DynamicType { get; }
        = new(ClassId.kDynamicCid) { Description = "<dynamic type>" };

    public DartAbstractType VoidType { get; }
        = new(ClassId.kVoidCid) { Description = "<void type>" };

    public DartTypeArguments EmptyTypeArguments { get; }
         = new() { Description = "<empty_type_arguments>" };

    public DartBool True { get; } = new(true);

    public DartBool False { get; } = new(false);

    public DartArray SyntheticGetterParameterTypes { get; }
        = new(ClassId.kArrayCid) { Description = "<synthetic getter parameter types>" };

    public DartArray SyntheticGetterParameterNames { get; }
        = new(ClassId.kArrayCid) { Description = "<synthetic getter parameter names>" };

    public DartArray ExtractorParameterTypes { get; }
        = new(ClassId.kArrayCid) { Description = "<extractor parameter types>" };

    public DartArray ExtractorParameterNames { get; }
        = new(ClassId.kArrayCid) { Description = "<extractor parameter names>" };

    public DartContext EmptyContext { get; }
        = new() { Description = "<empty_context>" };

    public DartContextScope EmptyContextScope { get; }
        = new() { Description = "<empty_context_scope>" };

    public DartObjectPool EmptyObjectPool { get; }
         = new() { Description = "<empty_object_pool>" };

    public DartCompressedStackMaps EmptyCompressedStackMaps { get; }
        = new() { Description = "<empty_compressed_stackmaps>" };

    public DartPcDescriptors EmptyPcDescriptors { get; }
        = new() { Description = "<empty_descriptors>" };

    public DartLocalVarDescriptors EmptyVarDescriptors { get; }
        = new() { Description = "<empty_var_descriptors>" };

    public DartExceptionHandlers EmptyExceptionHandlers { get; }
        = new() { Description = "<empty_exception_handlers>" };

    public DartExceptionHandlers EmptyAsyncExceptionHandlers { get; }
        = new() { Description = "<empty_async_exception_handlers>" };

    public DartBytecode ImplicitGetterBytecode { get; }
        = new() { Description = "ImplicitGetterBytecode" };

    public DartBytecode ImplicitSetterBytecode { get; }
        = new() { Description = "ImplicitSetterBytecode" };

    public DartBytecode ImplicitStaticGetterBytecode { get; }
        = new() { Description = "ImplicitStaticGetterBytecode" };

    public DartBytecode MethodExtractorBytecode { get; }
        = new() { Description = "MethodExtractorBytecode" };

    public DartBytecode InvokeClosureBytecode { get; }
        = new() { Description = "InvokeClosureBytecode" };

    public DartBytecode InvokeFieldBytecode { get; }
        = new() { Description = "InvokeFieldBytecode" };

    public DartBytecode NsmDispatcherBytecode { get; }
        = new() { Description = "NsmDispatcherBytecode" };

    public DartBytecode DynamicInvocationForwarderBytecode { get; }
        = new() { Description = "DynamicInvocationForwarderBytecode" };

    public DartAbstractType NeverType { get; }
        = new(ClassId.kNeverCid) { Description = "<never type>" };

    public ImmutableArray<DartArray> CachedArgsDescriptors { get; } = [
        .. new[] { 31, 2 }.SelectMany((n, i) =>
            Enumerable.Range(1, n).Select(n =>
                new DartArray()
                {
                    Length = (ulong)n,
                    Description = $"<cached arguments descriptor[{i},{n}]>",
                    Data = new DartObject[n]
                }
            )
        )
    ];

    public ImmutableArray<DartArray> CachedICDataArrays { get; } = [
        .. Enumerable.Range(0, 4).Select(n =>
            new DartArray(ClassId.kArrayCid)
            {
                Description = $"<empty icdata entries[{n}]>",
                Length = (ulong)n,
                Data = new DartObject[n]
            }
        )
    ];

    public ImmutableArray<DartArray> CachedICDataArrays_Pre221 { get; } = [
        .. Enumerable.Range(0, 4).Select(n =>
            new DartArray(ClassId.kICDataCid)
            {
                Description = $"<empty icdata entries[{n}]>",
                Length = (ulong)n,
                Data = new DartObject[n]
            }
        )
    ];
    #endregion

    public ClassTable()
    {
        this.EnsureCapacity(512);
    }

    public ClassTable(ClassTable other, List<DartObject> baseObjects) : base(other)
    {
        this.BaseObjects = baseObjects;
        this.ClassIdLookup = other.ClassIdLookup;
        this.Version = other.Version;
        this.SnapshotVersion = other.SnapshotVersion;
    }

    public void Init(Snapshot snapshot)
    {
        if (this.Count != 0)
        {
            Debug.Assert(snapshot.SnapshotVersion == this.SnapshotVersion);
            Debug.Assert(snapshot.Version == this.Version);
        }
        else
        {
            this.SnapshotVersion = snapshot.SnapshotVersion;
            this.Version = snapshot.Version;
            this.Is64Bit = snapshot.Is64Bit;

            ClassIdLookup = InitClassIdLookup(snapshot.Version);

            foreach (var classid in ClassIdLookup)
            {
                this[classid] = new DartClass(classid);
            }

            BaseObjects = InitBaseObjects(snapshot);
        }

        if (snapshot.Objects.Count == 0)
        {
            foreach (var obj in BaseObjects)
            {
                snapshot.AddObject(obj);
            }
        }
    }

    private List<DartObject> InitBaseObjects(Snapshot snapshot)
    {
        List<DartObject> baseObjects = [
            this.Null,
            this.Sentinel,
            this.OptimizedOut,
            this.EmptyArray,
            this.EmptyInstantiationsCacheArray,
            this.EmptySubtypeTestCacheArray,
            this.DynamicType,
            this.VoidType,
            this.EmptyTypeArguments,
            this.True,
            this.False,
            this.SyntheticGetterParameterTypes,
            this.SyntheticGetterParameterNames,
            this.EmptyContextScope,
            this.EmptyObjectPool,
            this.EmptyCompressedStackMaps,
            this.EmptyPcDescriptors,
            this.EmptyVarDescriptors,
            this.EmptyExceptionHandlers,
            this.EmptyAsyncExceptionHandlers,
            .. this.CachedArgsDescriptors,
            .. snapshot.Version.ComparePrecedenceTo(SemVersion.Parse("2.2.1-dev.2.0-edge-32+2c001aa")) >= 0 ? this.CachedICDataArrays : this.CachedICDataArrays_Pre221,
        ];

        var firstInternalOnlyIndex = this.ClassIdLookup.IndexOf(kFirstInternalOnlyCid);
        var lastInternalOnlyIndex = this.ClassIdLookup.IndexOf(kLastInternalOnlyCid);

        for (int i = firstInternalOnlyIndex; i < lastInternalOnlyIndex; i++)
        {
            var cid = this.ClassIdLookup[i];
            if (cid != ClassId.kErrorCid && cid != ClassId.kCallSiteDataCid)
            {
                baseObjects.Add(this[ClassIdLookup[i]]);
            }
        }

        baseObjects.Add(this[ClassId.kDynamicCid]);
        baseObjects.Add(this[ClassId.kVoidCid]);

        if (snapshot.Version.ComparePrecedenceTo(SemVersion.Parse("1.22.0-dev.10.0-edge-436+bee82fe")) >= 0
            && snapshot.Kind is not SnapshotKind.kFullJIT or SnapshotKind.kFullAOT or SnapshotKind.kModule)
        {

        }

        foreach (var (changeVer, (adds, dels)) in VersionTable.BaseObjectChanges.OrderByDescending(e => e.Key, SemVersion.PrecedenceComparer))
        {
            if (snapshot.Version.ComparePrecedenceTo(changeVer) >= 0)
                return baseObjects;

            foreach (var (add, _) in adds)
            {
                baseObjects.Remove(add(this));
            }

            foreach (var (del, after) in dels)
            {
                baseObjects.Insert(baseObjects.IndexOf(after(this)), del(this));
            }
        }

        return baseObjects;
    }

    private static List<ClassId> InitClassIdLookup(SemVersion version)
    {
        var classids = new List<ClassId>();

        for (ClassId cid = ClassId.kIllegalCid; cid < ClassId.kNumPredefinedCids; cid++)
        {
            Debug.Assert((ulong)classids.Count == (ulong)cid);
            classids.Add(cid);
        }

        foreach (var (changeVer, (adds, dels)) in VersionTable.ClassIdChanges.AsEnumerable().Reverse())
        {
            if (version.ComparePrecedenceTo(changeVer) >= 0)
                return classids;

            foreach (var (add, _) in adds)
            {
                classids.Remove(add);
            }

            foreach (var (del, after) in dels)
            {
                classids.Insert(classids.IndexOf(after) + 1, del);
            }
        }

        return classids;
    }

    public ClassId LookupClassId(ulong cid)
    {
        if (cid < (ulong)ClassIdLookup.Count)
        {
            return ClassIdLookup[(int)cid];
        }

        if (cid >= (ulong)ClassId.kTopLevelCidOffset)
        {
            return (ClassId)cid;
        }

        return cid - (ulong)ClassIdLookup.Count + ClassId.kNumPredefinedCids;
    }

    public ObjectClassIdTag DecodeObjectTag(ulong tag)
    {
        ObjectTagFlags flags = 0;
        uint size = 0;
        ulong cidval = 0;
        uint hash = 0;
        List<ObjectTagFlags> flagBits = [];
        SizeShiftBits shiftMasks = new(8, 0x0F, 12, 0xFFFFF, 32, 0xFFFFFFFF);

        foreach (var (version, bits) in VersionTable.ObjectFlagBits.OrderByDescending(e => e.Key, SemVersion.PrecedenceComparer))
        {
            flagBits = bits;

            if (Version?.ComparePrecedenceTo(version) > 0)
            {
                break;
            }
        }

        foreach (var (version, (sm32, sm64)) in VersionTable.ObjectTagSizeShift.OrderByDescending(e => e.Key, SemVersion.PrecedenceComparer))
        {
            shiftMasks = Is64Bit ? sm64 : sm32;

            if (Version?.ComparePrecedenceTo(version) > 0)
            {
                break;
            }
        }

        for (int bit = 0; bit < flagBits.Count; bit++)
        {
            if ((tag & (1u << bit)) != 0)
            {
                flags |= flagBits[bit];
            }
        }

        size = (uint)((tag >> shiftMasks.SizeShift) & shiftMasks.SizeMask);
        cidval = (tag >> shiftMasks.ClassIdShift) & shiftMasks.ClassIdMask;
        hash = (uint)((tag >> shiftMasks.HashShift) & shiftMasks.HashMask);

        return new ObjectClassIdTag
        {
            RawTag = tag,
            Flags = flags,
            Size = size,
            ClassId = LookupClassId(cidval),
            Hash = hash
        };
    }

    public static ClassTable CloneFromVMIsolate(DartIsolate vmIsolate)
    {
        return new ClassTable(vmIsolate.ClassTable, vmIsolate.Objects);
    }
}
