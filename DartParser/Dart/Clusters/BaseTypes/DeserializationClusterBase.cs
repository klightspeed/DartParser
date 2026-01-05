using DartParser.Dart.Clusters.Canonical;
using DartParser.Dart.Clusters.FixedSize;
using DartParser.Dart.Clusters.Other;
using DartParser.Dart.Clusters.VariableLength;
using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.VariableLength;
using Semver;
using System.Runtime.Intrinsics;

namespace DartParser.Dart.Clusters.BaseTypes;

public class DeserializationClusterBase(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, SemVersion version)
{
    public bool IsCanonical { get; } = isCanonical;
    public bool IsImmutable { get; } = isImmutable;
    public bool IsRootUnit { get; } = isRootUnit;
    public ClassId ClassId { get; } = cid;
    public SemVersion Version { get; } = version;

    public virtual void ReadAllocate(Snapshot snapshot)
    {
        throw new NotImplementedException();
    }

    public virtual void ReadFill(Snapshot snapshot)
    {
        throw new NotImplementedException();
    }

    public static DeserializationClusterBase Create(Snapshot snapshot)
    {
        var tags = snapshot.Read<ulong>();
        var cardRemembered = (tags & 1) != 0;
        var isCanonical = (tags & 2) != 0;
        var notMarked = (tags & 4) != 0;
        var newOrEvacuationCandidate = (tags & 8) != 0;
        var alwaysSet = (tags & 16) != 0;
        var oldAndNotRemembered = (tags & 32) != 0;
        var isImmutable = (tags & 64) != 0;
        var size = (tags >> 8) & 15;
        var isRootUnit = !snapshot.IsNonRootUnit;
        var cidval = (tags >> 12) & 0xFFFFF;
        var cid = snapshot.Isolate.ClassTable.LookupClassId(cidval);
        var version = snapshot.Version;

        return cid switch
        {
            (>= ClassId.kNumPredefinedCids and < ClassId.DeletedCidStart)
                or ClassId.kInstanceCid
                    => new InstanceDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId c
                when c.IsTypedDataViewClassId()
                    => new TypedDataViewDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kPcDescriptorsCid
                when !snapshot.UseCompressedPointers
                  && snapshot.Kind.IncludesCode()
                    => new RODataDeserializationCluster<DartPcDescriptors>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kCodeSourceMapCid
                when !snapshot.UseCompressedPointers
                  && snapshot.Kind.IncludesCode()
                    => new RODataDeserializationCluster<DartCodeSourceMap>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kCompressedStackMapsCid
                when !snapshot.UseCompressedPointers
                  && snapshot.Kind.IncludesCode()
                    => new RODataDeserializationCluster<DartCompressedStackMaps>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kOneByteStringCid
                or ClassId.kTwoByteStringCid
                or ClassId.kStringCid
                when !snapshot.UseCompressedPointers
                  && snapshot.Kind.IncludesCode()
                  && isRootUnit
                    => new ROStringDataDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypedDataInt8ArrayCid
                    => new TypedDataDeserializationCluster<sbyte>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypedDataUint8ArrayCid
                    => new TypedDataDeserializationCluster<byte>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypedDataUint8ClampedArrayCid
                    => new TypedDataDeserializationCluster<byte>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypedDataInt16ArrayCid
                    => new TypedDataDeserializationCluster<short>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypedDataUint16ArrayCid
                    => new TypedDataDeserializationCluster<ushort>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypedDataInt32ArrayCid
                    => new TypedDataDeserializationCluster<int>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypedDataUint32ArrayCid
                    => new TypedDataDeserializationCluster<uint>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypedDataInt64ArrayCid
                    => new TypedDataDeserializationCluster<long>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypedDataUint64ArrayCid
                    => new TypedDataDeserializationCluster<ulong>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypedDataFloat32ArrayCid
                    => new TypedDataDeserializationCluster<float>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypedDataFloat64ArrayCid
                    => new TypedDataDeserializationCluster<double>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypedDataInt32x4ArrayCid
                    => new TypedDataDeserializationCluster<Vector128<int>>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypedDataFloat32x4ArrayCid
                    => new TypedDataDeserializationCluster<Vector128<float>>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypedDataFloat64x2ArrayCid
                    => new TypedDataDeserializationCluster<Vector128<double>>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExternalTypedDataInt8ArrayCid
                    => new ExternalTypedDataDeserializationCluster<sbyte>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExternalTypedDataUint8ArrayCid
                    => new ExternalTypedDataDeserializationCluster<byte>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExternalTypedDataUint8ClampedArrayCid
                    => new ExternalTypedDataDeserializationCluster<byte>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExternalTypedDataInt16ArrayCid
                    => new ExternalTypedDataDeserializationCluster<short>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExternalTypedDataUint16ArrayCid
                    => new ExternalTypedDataDeserializationCluster<ushort>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExternalTypedDataInt32ArrayCid
                    => new ExternalTypedDataDeserializationCluster<int>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExternalTypedDataUint32ArrayCid
                    => new ExternalTypedDataDeserializationCluster<uint>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExternalTypedDataInt64ArrayCid
                    => new ExternalTypedDataDeserializationCluster<long>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExternalTypedDataUint64ArrayCid
                    => new ExternalTypedDataDeserializationCluster<ulong>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExternalTypedDataFloat32ArrayCid
                    => new ExternalTypedDataDeserializationCluster<float>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExternalTypedDataFloat64ArrayCid
                    => new ExternalTypedDataDeserializationCluster<double>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExternalTypedDataInt32x4ArrayCid
                    => new ExternalTypedDataDeserializationCluster<Vector128<int>>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExternalTypedDataFloat32x4ArrayCid
                    => new ExternalTypedDataDeserializationCluster<Vector128<float>>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExternalTypedDataFloat64x2ArrayCid
                    => new ExternalTypedDataDeserializationCluster<Vector128<double>>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kClassCid
                    => new ClassDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypeParametersCid
                    => new TypeParametersDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypeArgumentsCid
                    => new TypeArgumentsDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kPatchClassCid
                    => new PatchClassDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kFunctionCid
                    => new FunctionDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kClosureDataCid
                    => new ClosureDataDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kFfiTrampolineDataCid
                    => new FfiTrampolineDataDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kFieldCid
                    => new FieldDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kScriptCid
                    => new ScriptDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kLibraryCid
                    => new LibraryDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kNamespaceCid
                    => new NamespaceDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kKernelProgramInfoCid
                    => new KernelProgramInfoDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kCodeCid
                    => new CodeDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kObjectPoolCid
                    => new ObjectPoolDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kPcDescriptorsCid
                    => new PcDescriptorsDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kCodeSourceMapCid
                    => new CodeSourceMapDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kCompressedStackMapsCid
                    => new CompressedStackMapsDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kExceptionHandlersCid
                    => new ExceptionHandlersDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kContextCid
                    => new ContextDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kUnlinkedCallCid
                    => new UnlinkedCallDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kICDataCid
                    => new ICDataDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kMegamorphicCacheCid
                    => new MegamorphicCacheDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kSubtypeTestCacheCid
                    => new SubtypeTestCacheDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kLoadingUnitCid
                    => new LoadingUnitDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kLanguageErrorCid
                    => new LanguageErrorDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kUnhandledExceptionCid
                    => new UnhandledExceptionDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kLibraryPrefixCid
                    => new LibraryPrefixDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypeCid
                    => new TypeDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kFunctionTypeCid
                    => new FunctionTypeDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kRecordTypeCid
                    => new RecordTypeDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kTypeParameterCid
                    => new TypeParameterDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kClosureCid
                    => new ClosureDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kMintCid
                    => new MintDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kDoubleCid
                    => new DoubleDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kInt32x4Cid
                    => new Simd128DeserializationCluster<int>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kFloat32x4Cid
                    => new Simd128DeserializationCluster<float>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kFloat64x2Cid
                    => new Simd128DeserializationCluster<double>(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kGrowableObjectArrayCid
                    => new GrowableObjectArrayDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kRecordCid
                    => new RecordDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kStackTraceCid
                    => new StackTraceDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kRegExpCid
                    => new RegExpDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kWeakPropertyCid
                    => new WeakPropertyDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kConstMapCid
                    => new MapDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kConstSetCid
                    => new SetDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kArrayCid
                or ClassId.kImmutableArrayCid
                    => new ArrayDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kWeakArrayCid
                    => new WeakArrayDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassId.kStringCid
                    => new StringDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit && !snapshot.IsVMSnapshot, version),
            >= ClassTable.kFirstFfiTypeCid
                and <= ClassTable.kLastFfiTypeCid
                    => new InstanceDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            ClassTable.kDeltaEncodedTypedDataCid
                    => new DeltaEncodedTypedDataDeserializationCluster(isCanonical, isImmutable, cid, isRootUnit, version),
            _ => throw new InvalidDataException($"No cluster defined for cid {cid}"),
        };
        throw new NotImplementedException();
    }

    public static T[] ReadAllocateFixedSize<T>(Snapshot snapshot, ClassId cid)
        where T : DartObject, new()
    {
        var count = (int)snapshot.ReadUnsigned();
        var entries = new T[count];

        for (int i = 0; i < count; i++)
        {
            var entry = new T { Type = cid };
            snapshot.AddObject(entry);
            entries[i] = entry;
        }

        return entries;
    }

    public static T[] ReadAllocateVariableLength<T, U>(Snapshot snapshot, ClassId cid)
        where T : DartObject, IHasData<U>, new()
    {
        var count = (int)snapshot.ReadUnsigned();
        var entries = new T[count];

        for (int i = 0; i < count; i++)
        {
            var length = snapshot.ReadUnsigned();

            var entry = new T
            {
                Type = cid,
                Data = new U[(int)length]
            };
            snapshot.AddObject(entry);
            entries[i] = entry;
        }

        return entries;
    }

    public static void ReadFillFixedSize<T>(Snapshot snapshot, T[] entries)
        where T : DartObject, IHasPropertySetters<T>, new()
    {
        foreach (var entry in entries)
        {
            snapshot.FillFields(entry);
        }
    }

    public static void ReadFillVariableLengthRef<T, U>(Snapshot snapshot, T[] entries)
        where T : DartObject, IHasPropertySetters<T>, IHasData<U>, new()
        where U : DartObject
    {
        foreach (var entry in entries)
        {
            snapshot.FillFields(entry);

            for (var i = 0UL; i < entry.Length; i++)
            {
                entry.Data[i] = snapshot.ReadRef<U>();
            }
        }
    }

    public static void ReadFillVariableLengthRaw<T, U>(Snapshot snapshot, T[] entries)
        where T : DartObject, IHasPropertySetters<T>, IHasData<U>, new()
        where U : struct
    {
        foreach (var entry in entries)
        {
            snapshot.FillFields(entry);

            for (var i = 0UL; i < entry.Length; i++)
            {
                entry.Data[i] = snapshot.ReadRaw<U>();
            }
        }
    }

    public static void ReadFillVariableLengthWithFillNew<T, U>(Snapshot snapshot, T[] entries)
        where T : DartObject, IHasPropertySetters<T>, IHasData<U>, new()
        where U : IHasPropertySetters<U>, new()
    {
        foreach (var entry in entries)
        {
            snapshot.FillFields(entry);

            for (var i = 0UL; i < entry.Length; i++)
            {
                entry.Data[i] = snapshot.FillFields<U>();
            }
        }
    }

    public static void ReadFillVariableLengthWithFillRef<T, U>(Snapshot snapshot, T[] entries)
        where T : DartObject, IHasPropertySetters<T>, IHasData<U>, new()
        where U : struct, IHasPropertySetters<U>
    {
        foreach (var entry in entries)
        {
            snapshot.FillFields(entry);

            for (var i = 0UL; i < entry.Length; i++)
            {
                snapshot.FillFields(ref entry.Data[i]);
            }
        }
    }
}

