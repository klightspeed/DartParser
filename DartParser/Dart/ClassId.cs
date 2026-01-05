using System;
using System.Collections.Generic;
using System.Text;

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
        kIllegalCid = 0,
        kNativePointer,
        kFreeListElement,
        kForwardingCorpse,
        kObjectCid,
        kClassCid,
        kPatchClassCid,
        kFunctionCid,
        kTypeParametersCid,
        kClosureDataCid,
        kFfiTrampolineDataCid,
        kFieldCid,
        kScriptCid,
        kLibraryCid,
        kNamespaceCid,
        kKernelProgramInfoCid,
        kWeakSerializationReferenceCid,
        kWeakArrayCid,
        kCodeCid,
        kBytecodeCid,
        kInstructionsCid,
        kInstructionsSectionCid,
        kInstructionsTableCid,
        kObjectPoolCid,
        kPcDescriptorsCid,
        kCodeSourceMapCid,
        kCompressedStackMapsCid,
        kLocalVarDescriptorsCid,
        kExceptionHandlersCid,
        kContextCid,
        kContextScopeCid,
        kSentinelCid,
        kSingleTargetCacheCid,
        kMonomorphicSmiableCallCid,
        kCallSiteDataCid,
        kUnlinkedCallCid,
        kICDataCid,
        kMegamorphicCacheCid,
        kSubtypeTestCacheCid,
        kLoadingUnitCid,
        kErrorCid,
        kApiErrorCid,
        kLanguageErrorCid,
        kUnhandledExceptionCid,
        kUnwindErrorCid,
        kInstanceCid,
        kLibraryPrefixCid,
        kTypeArgumentsCid,
        kAbstractTypeCid,
        kTypeCid,
        kFunctionTypeCid,
        kRecordTypeCid,
        kTypeParameterCid,
        kFinalizerBaseCid,
        kFinalizerCid,
        kNativeFinalizerCid,
        kFinalizerEntryCid,
        kClosureCid,
        kNumberCid,
        kIntegerCid,
        kSmiCid,
        kMintCid,
        kDoubleCid,
        kBoolCid,
        kFloat32x4Cid,
        kInt32x4Cid,
        kFloat64x2Cid,
        kRecordCid,
        kTypedDataBaseCid,
        kTypedDataCid,
        kExternalTypedDataCid,
        kTypedDataViewCid,
        kPointerCid,
        kDynamicLibraryCid,
        kCapabilityCid,
        kReceivePortCid,
        kSendPortCid,
        kStackTraceCid,
        kSuspendStateCid,
        kRegExpCid,
        kWeakPropertyCid,
        kWeakReferenceCid,
        kMirrorReferenceCid,
        kFutureOrCid,
        kUserTagCid,
        kTransferableTypedDataCid,
        kMapCid,
        kConstMapCid,
        kSetCid,
        kConstSetCid,
        kArrayCid,
        kImmutableArrayCid,
        kGrowableObjectArrayCid,
        kStringCid,
        kOneByteStringCid,
        kTwoByteStringCid,
        kFfiNativeFunctionCid,
        kFfiInt8Cid,
        kFfiInt16Cid,
        kFfiInt32Cid,
        kFfiInt64Cid,
        kFfiUint8Cid,
        kFfiUint16Cid,
        kFfiUint32Cid,
        kFfiUint64Cid,
        kFfiFloatCid,
        kFfiDoubleCid,
        kFfiVoidCid,
        kFfiHandleCid,
        kFfiBoolCid,
        kFfiNativeTypeCid,
        kFfiStructCid,
        kTypedDataInt8ArrayCid,
        kTypedDataInt8ArrayViewCid = kTypedDataInt8ArrayCid + TypedDataCidRemainder.View,
        kExternalTypedDataInt8ArrayCid = kTypedDataInt8ArrayCid + TypedDataCidRemainder.External,
        kUnmodifiableTypedDataInt8ArrayViewCid = kTypedDataInt8ArrayCid + TypedDataCidRemainder.Unmodifiable,
        kTypedDataUint8ArrayCid,
        kTypedDataUint8ArrayViewCid = kTypedDataUint8ArrayCid + TypedDataCidRemainder.View,
        kExternalTypedDataUint8ArrayCid = kTypedDataUint8ArrayCid + TypedDataCidRemainder.External,
        kUnmodifiableTypedDataUint8ArrayViewCid = kTypedDataUint8ArrayCid + TypedDataCidRemainder.Unmodifiable,
        kTypedDataUint8ClampedArrayCid,
        kTypedDataUint8ClampedArrayViewCid = kTypedDataUint8ClampedArrayCid + TypedDataCidRemainder.View,
        kExternalTypedDataUint8ClampedArrayCid = kTypedDataUint8ClampedArrayCid + TypedDataCidRemainder.External,
        kUnmodifiableTypedDataUint8ClampedArrayViewCid = kTypedDataUint8ClampedArrayCid + TypedDataCidRemainder.Unmodifiable,
        kTypedDataInt16ArrayCid,
        kTypedDataInt16ArrayViewCid = kTypedDataInt16ArrayCid + TypedDataCidRemainder.View,
        kExternalTypedDataInt16ArrayCid = kTypedDataInt16ArrayCid + TypedDataCidRemainder.External,
        kUnmodifiableTypedDataInt16ArrayViewCid = kTypedDataInt16ArrayCid + TypedDataCidRemainder.Unmodifiable,
        kTypedDataUint16ArrayCid,
        kTypedDataUint16ArrayViewCid = kTypedDataUint16ArrayCid + TypedDataCidRemainder.View,
        kExternalTypedDataUint16ArrayCid = kTypedDataUint16ArrayCid + TypedDataCidRemainder.External,
        kUnmodifiableTypedDataUint16ArrayViewCid = kTypedDataUint16ArrayCid + TypedDataCidRemainder.Unmodifiable,
        kTypedDataInt32ArrayCid,
        kTypedDataInt32ArrayViewCid = kTypedDataInt32ArrayCid + TypedDataCidRemainder.View,
        kExternalTypedDataInt32ArrayCid = kTypedDataInt32ArrayCid + TypedDataCidRemainder.External,
        kUnmodifiableTypedDataInt32ArrayViewCid = kTypedDataInt32ArrayCid + TypedDataCidRemainder.Unmodifiable,
        kTypedDataUint32ArrayCid,
        kTypedDataUint32ArrayViewCid = kTypedDataUint32ArrayCid + TypedDataCidRemainder.View,
        kExternalTypedDataUint32ArrayCid = kTypedDataUint32ArrayCid + TypedDataCidRemainder.External,
        kUnmodifiableTypedDataUint32ArrayViewCid = kTypedDataUint32ArrayCid + TypedDataCidRemainder.Unmodifiable,
        kTypedDataInt64ArrayCid,
        kTypedDataInt64ArrayViewCid = kTypedDataInt64ArrayCid + TypedDataCidRemainder.View,
        kExternalTypedDataInt64ArrayCid = kTypedDataInt64ArrayCid + TypedDataCidRemainder.External,
        kUnmodifiableTypedDataInt64ArrayViewCid = kTypedDataInt64ArrayCid + TypedDataCidRemainder.Unmodifiable,
        kTypedDataUint64ArrayCid,
        kTypedDataUint64ArrayViewCid = kTypedDataUint64ArrayCid + TypedDataCidRemainder.View,
        kExternalTypedDataUint64ArrayCid = kTypedDataUint64ArrayCid + TypedDataCidRemainder.External,
        kUnmodifiableTypedDataUint64ArrayViewCid = kTypedDataUint64ArrayCid + TypedDataCidRemainder.Unmodifiable,
        kTypedDataFloat32ArrayCid,
        kTypedDataFloat32ArrayViewCid = kTypedDataFloat32ArrayCid + TypedDataCidRemainder.View,
        kExternalTypedDataFloat32ArrayCid = kTypedDataFloat32ArrayCid + TypedDataCidRemainder.External,
        kUnmodifiableTypedDataFloat32ArrayViewCid = kTypedDataFloat32ArrayCid + TypedDataCidRemainder.Unmodifiable,
        kTypedDataFloat64ArrayCid,
        kTypedDataFloat64ArrayViewCid = kTypedDataFloat64ArrayCid + TypedDataCidRemainder.View,
        kExternalTypedDataFloat64ArrayCid = kTypedDataFloat64ArrayCid + TypedDataCidRemainder.External,
        kUnmodifiableTypedDataFloat64ArrayViewCid = kTypedDataFloat64ArrayCid + TypedDataCidRemainder.Unmodifiable,
        kTypedDataFloat32x4ArrayCid,
        kTypedDataFloat32x4ArrayViewCid = kTypedDataFloat32x4ArrayCid + TypedDataCidRemainder.View,
        kExternalTypedDataFloat32x4ArrayCid = kTypedDataFloat32x4ArrayCid + TypedDataCidRemainder.External,
        kUnmodifiableTypedDataFloat32x4ArrayViewCid = kTypedDataFloat32x4ArrayCid + TypedDataCidRemainder.Unmodifiable,
        kTypedDataInt32x4ArrayCid,
        kTypedDataInt32x4ArrayViewCid = kTypedDataInt32x4ArrayCid + TypedDataCidRemainder.View,
        kExternalTypedDataInt32x4ArrayCid = kTypedDataInt32x4ArrayCid + TypedDataCidRemainder.External,
        kUnmodifiableTypedDataInt32x4ArrayViewCid = kTypedDataInt32x4ArrayCid + TypedDataCidRemainder.Unmodifiable,
        kTypedDataFloat64x2ArrayCid,
        kTypedDataFloat64x2ArrayViewCid = kTypedDataFloat64x2ArrayCid + TypedDataCidRemainder.View,
        kExternalTypedDataFloat64x2ArrayCid = kTypedDataFloat64x2ArrayCid + TypedDataCidRemainder.External,
        kUnmodifiableTypedDataFloat64x2ArrayViewCid = kTypedDataFloat64x2ArrayCid + TypedDataCidRemainder.Unmodifiable,
        kByteDataViewCid,
        kUnmodifiableByteDataViewCid,
        kByteBufferCid,
        kNullCid,
        kDynamicCid,
        kVoidCid,
        kNeverCid,
        kNumPredefinedCids,

        DeletedCidStart = 0x1_0000_0000,

        kExternalOneByteStringCid,
        kExternalTwoByteStringCid,
        kTypeRefCid,
        kFfiPointerCid,
        kFfiDynamicLibraryCid,
        kSignatureDataCid,
        kRedirectionDataCid,
        kWasmInt32Cid,
        kWasmInt64Cid,
        kWasmFloatCid,
        kWasmDoubleCid,
        kWasmVoidCid,
        kParameterTypeCheckCid,
        kImageHeaderCid,
        kStackMapCid,
        kStackCid,
        kFfiIntPtrCid,
        kMixinAppTypeCid,
        kBoundedTypeCid,
        kUnresolvedClassCid,
        kLiteralTokenCid,
        kTokenStreamCid,
        kNativeEntryDataCid,
        kBigintCid,
        kVectorCid,
        kJSRegExpCid,
        kDeoptInfoCid,

        RenamedCidFlag = 0x2_0000_0000,

        kLinkedHashMapCid = kMapCid | RenamedCidFlag,
        kImmutableLinkedHashMapCid = kConstMapCid | RenamedCidFlag,
        kLinkedHashSetCid = kSetCid | RenamedCidFlag,
        kImmutableLinkedHashSetCid = kConstSetCid | RenamedCidFlag,
        kStacktraceCid = kStackTraceCid | RenamedCidFlag,
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
