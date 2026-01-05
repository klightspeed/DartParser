using System.ComponentModel;

namespace DartParser.Dart
{
    public enum TypedDataCidRemainder : ulong
    {
        Invalid = ulong.MaxValue,
        Internal = 0,
        View,
        External,
        Unmodifiable,
        NumRemainders
    }

    public enum ClassId : ulong
    {
        [Description("Illegal")]
        kIllegalCid = 0,
        kNativePointer,
        kFreeListElement,
        kForwardingCorpse,
        [Description("Object")]
        kObjectCid,
        [Description("Class")]
        kClassCid,
        [Description("PatchClass")]
        kPatchClassCid,
        [Description("Function")]
        kFunctionCid,
        [Description("TypeParameters")]
        kTypeParametersCid,
        [Description("ClosureData")]
        kClosureDataCid,
        [Description("FfiTrampolineData")]
        kFfiTrampolineDataCid,
        [Description("Field")]
        kFieldCid,
        [Description("Script")]
        kScriptCid,
        [Description("Library")]
        kLibraryCid,
        [Description("Namespace")]
        kNamespaceCid,
        [Description("KernelProgramInfo")]
        kKernelProgramInfoCid,
        [Description("WeakSerializationReference")]
        kWeakSerializationReferenceCid,
        [Description("WeakArray")]
        kWeakArrayCid,
        [Description("Code")]
        kCodeCid,
        [Description("Bytecode")]
        kBytecodeCid,
        [Description("Instructions")]
        kInstructionsCid,
        [Description("InstructionsSection")]
        kInstructionsSectionCid,
        [Description("InstructionsTable")]
        kInstructionsTableCid,
        [Description("ObjectPool")]
        kObjectPoolCid,
        [Description("PcDescriptors")]
        kPcDescriptorsCid,
        [Description("CodeSourceMap")]
        kCodeSourceMapCid,
        [Description("CompressedStackMaps")]
        kCompressedStackMapsCid,
        [Description("LocalVarDescriptors")]
        kLocalVarDescriptorsCid,
        [Description("ExceptionHandlers")]
        kExceptionHandlersCid,
        [Description("Context")]
        kContextCid,
        [Description("ContextScope")]
        kContextScopeCid,
        [Description("Sentinel")]
        kSentinelCid,
        [Description("SingleTargetCache")]
        kSingleTargetCacheCid,
        [Description("MonomorphicSmiableCall")]
        kMonomorphicSmiableCallCid,
        [Description("CallSiteData")]
        kCallSiteDataCid,
        [Description("UnlinkedCall")]
        kUnlinkedCallCid,
        [Description("ICData")]
        kICDataCid,
        [Description("MegamorphicCache")]
        kMegamorphicCacheCid,
        [Description("SubtypeTestCache")]
        kSubtypeTestCacheCid,
        [Description("LoadingUnit")]
        kLoadingUnitCid,
        [Description("Error")]
        kErrorCid,
        [Description("ApiError")]
        kApiErrorCid,
        [Description("LanguageError")]
        kLanguageErrorCid,
        [Description("UnhandledException")]
        kUnhandledExceptionCid,
        [Description("UnwindError")]
        kUnwindErrorCid,
        [Description("Instance")]
        kInstanceCid,
        [Description("LibraryPrefix")]
        kLibraryPrefixCid,
        [Description("TypeArguments")]
        kTypeArgumentsCid,
        [Description("AbstractType")]
        kAbstractTypeCid,
        [Description("Type")]
        kTypeCid,
        [Description("FunctionType")]
        kFunctionTypeCid,
        [Description("RecordType")]
        kRecordTypeCid,
        [Description("TypeParameter")]
        kTypeParameterCid,
        [Description("FinalizerBase")]
        kFinalizerBaseCid,
        [Description("Finalizer")]
        kFinalizerCid,
        [Description("NativeFinalizer")]
        kNativeFinalizerCid,
        [Description("FinalizerEntry")]
        kFinalizerEntryCid,
        [Description("Closure")]
        kClosureCid,
        [Description("Number")]
        kNumberCid,
        [Description("Integer")]
        kIntegerCid,
        [Description("Smi")]
        kSmiCid,
        [Description("Mint")]
        kMintCid,
        [Description("Double")]
        kDoubleCid,
        [Description("Bool")]
        kBoolCid,
        [Description("Float32x4")]
        kFloat32x4Cid,
        [Description("Int32x4")]
        kInt32x4Cid,
        [Description("Float64x2")]
        kFloat64x2Cid,
        [Description("Record")]
        kRecordCid,
        [Description("TypedDataBase")]
        kTypedDataBaseCid,
        [Description("TypedData")]
        kTypedDataCid,
        [Description("ExternalTypedData")]
        kExternalTypedDataCid,
        [Description("TypedDataView")]
        kTypedDataViewCid,
        [Description("Pointer")]
        kPointerCid,
        [Description("DynamicLibrary")]
        kDynamicLibraryCid,
        [Description("Capability")]
        kCapabilityCid,
        [Description("ReceivePort")]
        kReceivePortCid,
        [Description("SendPort")]
        kSendPortCid,
        [Description("StackTrace")]
        kStackTraceCid,
        [Description("SuspendState")]
        kSuspendStateCid,
        [Description("RegExp")]
        kRegExpCid,
        [Description("WeakProperty")]
        kWeakPropertyCid,
        [Description("WeakReference")]
        kWeakReferenceCid,
        [Description("MirrorReference")]
        kMirrorReferenceCid,
        [Description("FutureOr")]
        kFutureOrCid,
        [Description("UserTag")]
        kUserTagCid,
        [Description("TransferableTypedData")]
        kTransferableTypedDataCid,
        [Description("Map")]
        kMapCid,
        [Description("ConstMap")]
        kConstMapCid,
        [Description("Set")]
        kSetCid,
        [Description("ConstSet")]
        kConstSetCid,
        [Description("Array")]
        kArrayCid,
        [Description("ImmutableArray")]
        kImmutableArrayCid,
        [Description("GrowableObjectArray")]
        kGrowableObjectArrayCid,
        [Description("String")]
        kStringCid,
        [Description("OneByteString")]
        kOneByteStringCid,
        [Description("TwoByteString")]
        kTwoByteStringCid,
        [Description("FfiNativeFunction")]
        kFfiNativeFunctionCid,
        [Description("FfiInt8")]
        kFfiInt8Cid,
        [Description("FfiInt16")]
        kFfiInt16Cid,
        [Description("FfiInt32")]
        kFfiInt32Cid,
        [Description("FfiInt64")]
        kFfiInt64Cid,
        [Description("FfiUint8")]
        kFfiUint8Cid,
        [Description("FfiUint16")]
        kFfiUint16Cid,
        [Description("FfiUint32")]
        kFfiUint32Cid,
        [Description("FfiUint64")]
        kFfiUint64Cid,
        [Description("FfiFloat")]
        kFfiFloatCid,
        [Description("FfiDouble")]
        kFfiDoubleCid,
        [Description("FfiVoid")]
        kFfiVoidCid,
        [Description("FfiHandle")]
        kFfiHandleCid,
        [Description("FfiBool")]
        kFfiBoolCid,
        [Description("FfiNativeType")]
        kFfiNativeTypeCid,
        [Description("FfiStruct")]
        kFfiStructCid,
        [Description("TypedDataInt8Array")]
        kTypedDataInt8ArrayCid,
        [Description("TypedDataInt8ArrayView")]
        kTypedDataInt8ArrayViewCid = kTypedDataInt8ArrayCid + TypedDataCidRemainder.View,
        [Description("ExternalTypedDataInt8Array")]
        kExternalTypedDataInt8ArrayCid = kTypedDataInt8ArrayCid + TypedDataCidRemainder.External,
        [Description("UnmodifiableTypedDataInt8ArrayView")]
        kUnmodifiableTypedDataInt8ArrayViewCid = kTypedDataInt8ArrayCid + TypedDataCidRemainder.Unmodifiable,
        [Description("TypedDataUint8Array")]
        kTypedDataUint8ArrayCid,
        [Description("TypedDataUint8ArrayView")]
        kTypedDataUint8ArrayViewCid = kTypedDataUint8ArrayCid + TypedDataCidRemainder.View,
        [Description("ExternalTypedDataUint8Array")]
        kExternalTypedDataUint8ArrayCid = kTypedDataUint8ArrayCid + TypedDataCidRemainder.External,
        [Description("UnmodifiableTypedDataUint8ArrayView")]
        kUnmodifiableTypedDataUint8ArrayViewCid = kTypedDataUint8ArrayCid + TypedDataCidRemainder.Unmodifiable,
        [Description("TypedDataUint8ClampedArray")]
        kTypedDataUint8ClampedArrayCid,
        [Description("TypedDataUint8ClampedArrayView")]
        kTypedDataUint8ClampedArrayViewCid = kTypedDataUint8ClampedArrayCid + TypedDataCidRemainder.View,
        [Description("ExternalTypedDataUint8ClampedArray")]
        kExternalTypedDataUint8ClampedArrayCid = kTypedDataUint8ClampedArrayCid + TypedDataCidRemainder.External,
        [Description("UnmodifiableTypedDataUint8ClampedArrayView")]
        kUnmodifiableTypedDataUint8ClampedArrayViewCid = kTypedDataUint8ClampedArrayCid + TypedDataCidRemainder.Unmodifiable,
        [Description("TypedDataInt16Array")]
        kTypedDataInt16ArrayCid,
        [Description("TypedDataInt16ArrayView")]
        kTypedDataInt16ArrayViewCid = kTypedDataInt16ArrayCid + TypedDataCidRemainder.View,
        [Description("ExternalTypedDataInt16Array")]
        kExternalTypedDataInt16ArrayCid = kTypedDataInt16ArrayCid + TypedDataCidRemainder.External,
        [Description("UnmodifiableTypedDataInt16ArrayView")]
        kUnmodifiableTypedDataInt16ArrayViewCid = kTypedDataInt16ArrayCid + TypedDataCidRemainder.Unmodifiable,
        [Description("TypedDataUint16Array")]
        kTypedDataUint16ArrayCid,
        [Description("TypedDataUint16ArrayView")]
        kTypedDataUint16ArrayViewCid = kTypedDataUint16ArrayCid + TypedDataCidRemainder.View,
        [Description("ExternalTypedDataUint16Array")]
        kExternalTypedDataUint16ArrayCid = kTypedDataUint16ArrayCid + TypedDataCidRemainder.External,
        [Description("UnmodifiableTypedDataUint16ArrayView")]
        kUnmodifiableTypedDataUint16ArrayViewCid = kTypedDataUint16ArrayCid + TypedDataCidRemainder.Unmodifiable,
        [Description("TypedDataInt32Array")]
        kTypedDataInt32ArrayCid,
        [Description("TypedDataInt32ArrayView")]
        kTypedDataInt32ArrayViewCid = kTypedDataInt32ArrayCid + TypedDataCidRemainder.View,
        [Description("ExternalTypedDataInt32Array")]
        kExternalTypedDataInt32ArrayCid = kTypedDataInt32ArrayCid + TypedDataCidRemainder.External,
        [Description("UnmodifiableTypedDataInt32ArrayView")]
        kUnmodifiableTypedDataInt32ArrayViewCid = kTypedDataInt32ArrayCid + TypedDataCidRemainder.Unmodifiable,
        [Description("TypedDataUint32Array")]
        kTypedDataUint32ArrayCid,
        [Description("TypedDataUint32ArrayView")]
        kTypedDataUint32ArrayViewCid = kTypedDataUint32ArrayCid + TypedDataCidRemainder.View,
        [Description("ExternalTypedDataUint32Array")]
        kExternalTypedDataUint32ArrayCid = kTypedDataUint32ArrayCid + TypedDataCidRemainder.External,
        [Description("UnmodifiableTypedDataUint32ArrayView")]
        kUnmodifiableTypedDataUint32ArrayViewCid = kTypedDataUint32ArrayCid + TypedDataCidRemainder.Unmodifiable,
        [Description("TypedDataInt64Array")]
        kTypedDataInt64ArrayCid,
        [Description("TypedDataInt64ArrayView")]
        kTypedDataInt64ArrayViewCid = kTypedDataInt64ArrayCid + TypedDataCidRemainder.View,
        [Description("ExternalTypedDataInt64Array")]
        kExternalTypedDataInt64ArrayCid = kTypedDataInt64ArrayCid + TypedDataCidRemainder.External,
        [Description("UnmodifiableTypedDataInt64ArrayView")]
        kUnmodifiableTypedDataInt64ArrayViewCid = kTypedDataInt64ArrayCid + TypedDataCidRemainder.Unmodifiable,
        [Description("TypedDataUint64Array")]
        kTypedDataUint64ArrayCid,
        [Description("TypedDataUint64ArrayView")]
        kTypedDataUint64ArrayViewCid = kTypedDataUint64ArrayCid + TypedDataCidRemainder.View,
        [Description("ExternalTypedDataUint64Array")]
        kExternalTypedDataUint64ArrayCid = kTypedDataUint64ArrayCid + TypedDataCidRemainder.External,
        [Description("UnmodifiableTypedDataUint64ArrayView")]
        kUnmodifiableTypedDataUint64ArrayViewCid = kTypedDataUint64ArrayCid + TypedDataCidRemainder.Unmodifiable,
        [Description("TypedDataFloat32Array")]
        kTypedDataFloat32ArrayCid,
        [Description("TypedDataFloat32ArrayView")]
        kTypedDataFloat32ArrayViewCid = kTypedDataFloat32ArrayCid + TypedDataCidRemainder.View,
        [Description("ExternalTypedDataFloat32Array")]
        kExternalTypedDataFloat32ArrayCid = kTypedDataFloat32ArrayCid + TypedDataCidRemainder.External,
        [Description("UnmodifiableTypedDataFloat32ArrayView")]
        kUnmodifiableTypedDataFloat32ArrayViewCid = kTypedDataFloat32ArrayCid + TypedDataCidRemainder.Unmodifiable,
        [Description("TypedDataFloat64Array")]
        kTypedDataFloat64ArrayCid,
        [Description("TypedDataFloat64ArrayView")]
        kTypedDataFloat64ArrayViewCid = kTypedDataFloat64ArrayCid + TypedDataCidRemainder.View,
        [Description("ExternalTypedDataFloat64Array")]
        kExternalTypedDataFloat64ArrayCid = kTypedDataFloat64ArrayCid + TypedDataCidRemainder.External,
        [Description("UnmodifiableTypedDataFloat64ArrayView")]
        kUnmodifiableTypedDataFloat64ArrayViewCid = kTypedDataFloat64ArrayCid + TypedDataCidRemainder.Unmodifiable,
        [Description("TypedDataFloat32x4Array")]
        kTypedDataFloat32x4ArrayCid,
        [Description("TypedDataFloat32x4ArrayView")]
        kTypedDataFloat32x4ArrayViewCid = kTypedDataFloat32x4ArrayCid + TypedDataCidRemainder.View,
        [Description("ExternalTypedDataFloat32x4Array")]
        kExternalTypedDataFloat32x4ArrayCid = kTypedDataFloat32x4ArrayCid + TypedDataCidRemainder.External,
        [Description("UnmodifiableTypedDataFloat32x4ArrayView")]
        kUnmodifiableTypedDataFloat32x4ArrayViewCid = kTypedDataFloat32x4ArrayCid + TypedDataCidRemainder.Unmodifiable,
        [Description("TypedDataInt32x4Array")]
        kTypedDataInt32x4ArrayCid,
        [Description("TypedDataInt32x4ArrayView")]
        kTypedDataInt32x4ArrayViewCid = kTypedDataInt32x4ArrayCid + TypedDataCidRemainder.View,
        [Description("ExternalTypedDataInt32x4Array")]
        kExternalTypedDataInt32x4ArrayCid = kTypedDataInt32x4ArrayCid + TypedDataCidRemainder.External,
        [Description("UnmodifiableTypedDataInt32x4ArrayView")]
        kUnmodifiableTypedDataInt32x4ArrayViewCid = kTypedDataInt32x4ArrayCid + TypedDataCidRemainder.Unmodifiable,
        [Description("TypedDataFloat64x2Array")]
        kTypedDataFloat64x2ArrayCid,
        [Description("TypedDataFloat64x2ArrayView")]
        kTypedDataFloat64x2ArrayViewCid = kTypedDataFloat64x2ArrayCid + TypedDataCidRemainder.View,
        [Description("ExternalTypedDataFloat64x2Array")]
        kExternalTypedDataFloat64x2ArrayCid = kTypedDataFloat64x2ArrayCid + TypedDataCidRemainder.External,
        [Description("UnmodifiableTypedDataFloat64x2ArrayView")]
        kUnmodifiableTypedDataFloat64x2ArrayViewCid = kTypedDataFloat64x2ArrayCid + TypedDataCidRemainder.Unmodifiable,
        [Description("ByteDataView")]
        kByteDataViewCid,
        [Description("UnmodifiableByteDataView")]
        kUnmodifiableByteDataViewCid,
        [Description("ByteBuffer")]
        kByteBufferCid,
        [Description("Null")]
        kNullCid,
        [Description("Dynamic")]
        kDynamicCid,
        [Description("Void")]
        kVoidCid,
        [Description("Never")]
        kNeverCid,
        [Description("NumPredefined")]
        kNumPredefinedCids,

        DeletedCidStart = 0x1_0000_0000,

        [Description("ExternalOneByteString")]
        kExternalOneByteStringCid,
        [Description("ExternalTwoByteString")]
        kExternalTwoByteStringCid,
        [Description("TypeRef")]
        kTypeRefCid,
        [Description("FfiPointer")]
        kFfiPointerCid,
        [Description("FfiDynamicLibrary")]
        kFfiDynamicLibraryCid,
        [Description("SignatureData")]
        kSignatureDataCid,
        [Description("RedirectionData")]
        kRedirectionDataCid,
        [Description("WasmInt32")]
        kWasmInt32Cid,
        [Description("WasmInt64")]
        kWasmInt64Cid,
        [Description("WasmFloat")]
        kWasmFloatCid,
        [Description("WasmDouble")]
        kWasmDoubleCid,
        [Description("WasmVoid")]
        kWasmVoidCid,
        [Description("ParameterTypeCheck")]
        kParameterTypeCheckCid,
        [Description("ImageHeader")]
        kImageHeaderCid,
        [Description("StackMap")]
        kStackMapCid,
        [Description("Stack")]
        kStackCid,
        [Description("FfiIntPtr")]
        kFfiIntPtrCid,
        [Description("MixinAppType")]
        kMixinAppTypeCid,
        [Description("BoundedType")]
        kBoundedTypeCid,
        [Description("UnresolvedClass")]
        kUnresolvedClassCid,
        [Description("LiteralToken")]
        kLiteralTokenCid,
        [Description("TokenStream")]
        kTokenStreamCid,
        [Description("NativeEntryData")]
        kNativeEntryDataCid,
        [Description("Bigint")]
        kBigintCid,
        [Description("Vector")]
        kVectorCid,
        [Description("JSRegExp")]
        kJSRegExpCid,
        [Description("DeoptInfo")]
        kDeoptInfoCid,

        RenamedCidFlag = 0x2_0000_0000,

        [Description("LinkedHashMap")]
        kLinkedHashMapCid = kMapCid | RenamedCidFlag,
        [Description("ImmutableLinkedHashMap")]
        kImmutableLinkedHashMapCid = kConstMapCid | RenamedCidFlag,
        [Description("LinkedHashSet")]
        kLinkedHashSetCid = kSetCid | RenamedCidFlag,
        [Description("ImmutableLinkedHashSet")]
        kImmutableLinkedHashSetCid = kConstSetCid | RenamedCidFlag,
        [Description("Stacktrace")]
        kStacktraceCid = kStackTraceCid | RenamedCidFlag,
        [Description("Stackmap")]
        kStackmapCid = kStackMapCid | RenamedCidFlag,

        kTopLevelCidOffset = 1048576,
    }

    public static class ClassTypeExtensions
    {
        extension(ClassId cid)
        {
            public bool IsInternalOnlyClassId()
                => cid <= ClassTable.kLastInternalOnlyCid;

            public bool IsCallSiteDataClassId()
                => cid >= ClassId.kCallSiteDataCid && cid <= ClassId.kMegamorphicCacheCid;

            public bool IsErrorClassId()
                => cid >= ClassTable.kFirstErrorCid && cid <= ClassTable.kLastErrorCid;

            public bool IsAbstractTypeClassId()
                => cid >= ClassId.kAbstractTypeCid && cid <= ClassId.kTypeParameterCid;

            public bool IsConcreteTypeClassId()
                => cid >= ClassId.kTypeCid && cid <= ClassId.kTypeParameterCid;

            public bool IsNumberClassId()
                => cid >= ClassId.kNumberCid && cid <= ClassId.kDoubleCid;

            public bool IsIntegerClassId()
                => cid >= ClassId.kIntegerCid && cid <= ClassId.kMintCid;

            public bool IsStringClassId()
                => cid >= ClassId.kStringCid && cid <= ClassId.kTwoByteStringCid;

            public bool IsOneByteStringClassId()
                => cid == ClassId.kOneByteStringCid;

            public bool IsArrayClassId()
                => cid >= ClassId.kArrayCid && cid <= ClassId.kGrowableObjectArrayCid;

            public bool IsBuiltinListClassId()
                => cid.IsArrayClassId() || cid.IsTypedDataBaseClassId() || cid == ClassId.kByteBufferCid;

            public bool IsTypeClassId()
                => cid == ClassId.kTypeCid || cid == ClassId.kFunctionTypeCid || cid == ClassId.kRecordTypeCid;

            public bool IsTypedDataBaseClassId()
                => cid >= ClassTable.kFirstTypedDataCid && cid <= ClassTable.kLastTypedDataCid;

            public ClassId GetTypedDataBaseClassInternalCid()
            {
                if (cid < ClassTable.kFirstTypedDataCid || cid > ClassTable.kLastTypedDataCid)
                    return cid;

                return cid - (ulong)cid.GetTypedDataCidRemainder() + (ulong)TypedDataCidRemainder.Internal;
            }

            public TypedDataCidRemainder GetTypedDataCidRemainder()
            {
                if (cid < ClassTable.kFirstTypedDataCid || cid > ClassTable.kLastTypedDataCid)
                    return TypedDataCidRemainder.Invalid;

                return (TypedDataCidRemainder)((cid - ClassTable.kFirstTypedDataCid) % (ulong)TypedDataCidRemainder.NumRemainders);
            }

            public bool IsTypedDataClassId()
                => cid.GetTypedDataCidRemainder() == TypedDataCidRemainder.Internal;

            public bool IsTypedDataViewClassId()
                => cid.GetTypedDataCidRemainder() == TypedDataCidRemainder.View;

            public bool IsExternalTypedDataClassId()
                => cid.GetTypedDataCidRemainder() == TypedDataCidRemainder.External;

            public bool IsUnmodifiableTypedDataClassId()
                => cid.GetTypedDataCidRemainder() == TypedDataCidRemainder.Unmodifiable;

            public bool IsClampedTypedDataBaseClassId()
                => cid.GetTypedDataBaseClassInternalCid() == ClassId.kTypedDataUint8ClampedArrayCid;

            public bool IsExternalPayloadClassId()
                => cid == ClassId.kPointerCid || cid.IsExternalTypedDataClassId();

            public bool IsDeeplyImmutableClassId()
                => cid.IsStringClassId()
                || cid == ClassId.kNumberCid
                || cid == ClassId.kIntegerCid
                || cid == ClassId.kSmiCid
                || cid == ClassId.kMintCid
                || cid == ClassId.kNeverCid
                || cid == ClassId.kSentinelCid
                || cid == ClassId.kStackTraceCid
                || cid == ClassId.kDoubleCid
                || cid == ClassId.kFloat32x4Cid
                || cid == ClassId.kFloat64x2Cid
                || cid == ClassId.kInt32x4Cid
                || cid == ClassId.kSendPortCid
                || cid == ClassId.kCapabilityCid
                || cid == ClassId.kRegExpCid
                || cid == ClassId.kBoolCid
                || cid == ClassId.kNullCid
                || cid == ClassId.kPointerCid
                || cid == ClassId.kTypeCid
                || cid == ClassId.kRecordTypeCid
                || cid == ClassId.kFunctionTypeCid;

            public bool IsShallowlyImmutableClassId()
                => cid == ClassId.kClosureCid
                || cid.IsUnmodifiableTypedDataClassId();

            public bool ShouldHaveImmutabilityBitSetClassId()
                => cid.IsDeeplyImmutableClassId() || cid.IsShallowlyImmutableClassId();

            public bool IsFfiTypeClassId()
                => cid == ClassId.kPointerCid
                || cid == ClassId.kFfiNativeFunctionCid
                || (cid >= ClassTable.kFirstFfiTypeCid && cid <= ClassTable.kLastFfiTypeCid);

            public bool IsFfiPredefinedClassId()
                => cid == ClassId.kPointerCid
                || cid == ClassId.kDynamicLibraryCid
                || (cid >= ClassTable.kFirstFfiCid && cid <= ClassTable.kLastFfiCid);

            public bool IsFfiPointerClassId()
                => cid == ClassId.kPointerCid;

            public bool IsFfiDynamicLibraryClassId()
                => cid == ClassId.kDynamicLibraryCid;

            public bool IsInternalVMDefinedClassId()
                => cid < ClassId.kNumPredefinedCids && !cid.IsImplicitFieldClassId();

            public bool IsImplicitFieldClassId()
                => cid == ClassId.kByteBufferCid;
        }
    }
}
