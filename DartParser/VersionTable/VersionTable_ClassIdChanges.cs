using DartParser.Dart;
using Semver;

namespace DartParser;

public static partial class VersionTable
{
    public static readonly Dictionary<SemVersion, AddsDels<ClassId>> ClassIdChanges = new()
    {
        [SemVersion.Parse("1.10.0-0-dev.1.0-edge-pre.3+8314062")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kDeoptInfoCid, After: ClassId.kExceptionHandlersCid),
            ]
        ),
        [SemVersion.Parse("1.12.0-0-dev.0.0-edge-pre.68+265a544")] = new(
            Adds: [
                new(Value: ClassId.kObjectPoolCid, After: ClassId.kInstructionsCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("1.15.0-0-dev.0.0-edge-pre.150+7f57ebc")] = new(
            Adds: [
                new(Value: ClassId.kFunctionTypeCid, After: ClassId.kTypeCid),
                new(Value: ClassId.kClosureCid, After: ClassId.kMixinAppTypeCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("1.15.0-0-dev.0.0-edge-pre.279+b525925")] = new(
            Adds: [
                new(Value: ClassId.kCodeSourceMapCid, After: ClassId.kPcDescriptorsCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("1.16.0-dev.1.0-edge-95+966aea0")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kFunctionTypeCid, After: ClassId.kTypeCid),
            ]
        ),
        [SemVersion.Parse("1.16.0-dev.1.0-edge-100+b070d98")] = new(
            Adds: [
                new(Value: ClassId.kRegExpCid, After: ClassId.kStacktraceCid),
            ],
            Dels: [
                new(Value: ClassId.kJSRegExpCid, After: ClassId.kStacktraceCid),
            ]
        ),
        [SemVersion.Parse("1.18.0-0-dev.0.0-edge-pre.116+3f8c0bb")] = new(
            Adds: [
                new(Value: ClassId.kForwardingCorpse, After: ClassId.kFreeListElement),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("1.20.0-dev.1.0-edge-38+ba94427")] = new(
            Adds: [
                new(Value: ClassId.kSingleTargetCacheCid, After: ClassId.kContextScopeCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("1.20.0-dev.1.0-edge-1234+7c74db2")] = new(
            Adds: [
                new(Value: ClassId.kFreeListElement, After: ClassId.kIllegalCid),
                new(Value: ClassId.kForwardingCorpse, After: ClassId.kFreeListElement),
            ],
            Dels: [
                new(Value: ClassId.kFreeListElement, After: ClassId.kVoidCid),
                new(Value: ClassId.kForwardingCorpse, After: ClassId.kFreeListElement),
            ]
        ),
        [SemVersion.Parse("1.20.0-dev.7.0-edge-109+2bc6e0a")] = new(
            Adds: [
                new(Value: ClassId.kUnlinkedCallCid, After: ClassId.kSingleTargetCacheCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("1.21.0-dev.10.0-edge-3+0cf0204")] = new(
            Adds: [
                new(Value: ClassId.kStackCid, After: ClassId.kIllegalCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("1.22.0-dev.1.0-edge-34+f125cc7")] = new(
            Adds: [
                new(Value: ClassId.kStackMapCid, After: ClassId.kCodeSourceMapCid),
                new(Value: ClassId.kStackTraceCid, After: ClassId.kSendPortCid),
            ],
            Dels: [
                new(Value: ClassId.kStackmapCid, After: ClassId.kCodeSourceMapCid),
                new(Value: ClassId.kStacktraceCid, After: ClassId.kSendPortCid),
            ]
        ),
        [SemVersion.Parse("1.22.0-dev.3.0-edge-1+889b003")] = new(
            Adds: [
                new(Value: ClassId.kSignatureDataCid, After: ClassId.kClosureDataCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("1.25.0-dev.7.0-edge-151+5475240")] = new(
            Adds: [
                new(Value: ClassId.kVectorCid, After: ClassId.kVoidCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("1.25.0-dev.14.0-edge-19+b3a7dd6")] = new(
            Adds: [
                new(Value: ClassId.kTypeArgumentsCid, After: ClassId.kLibraryPrefixCid),
            ],
            Dels: [
                new(Value: ClassId.kTypeArgumentsCid, After: ClassId.kUnresolvedClassCid),
            ]
        ),
        [SemVersion.Parse("2.0.0-dev.3.0-edge-463+5558fc4")] = new(
            Adds: [
                new(Value: ClassId.kKernelProgramInfoCid, After: ClassId.kNamespaceCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.0.0-dev.53.0-edge-52+a6b6d6a")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kVectorCid, After: ClassId.kVoidCid),
            ]
        ),
        [SemVersion.Parse("2.0.0-dev.60.0-edge-9+c3b5939")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kBigintCid, After: ClassId.kMintCid),
            ]
        ),
        [SemVersion.Parse("2.1.0-dev.3.0-edge-99+35d8f0b")] = new(
            Adds: [
                new(Value: ClassId.kNativeEntryDataCid, After: ClassId.kRedirectionDataCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.1.0-dev.4.0-edge-159+a6a48f2")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kNativeEntryDataCid, After: ClassId.kRedirectionDataCid),
            ]
        ),
        [SemVersion.Parse("2.1.0-dev.7.0-edge-69+662ff7f")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kLiteralTokenCid, After: ClassId.kFieldCid),
                new(Value: ClassId.kTokenStreamCid, After: ClassId.kLiteralTokenCid),
            ]
        ),
        [SemVersion.Parse("2.1.1-0-dev.0.0-edge-352+f721d52")] = new(
            Adds: [
                new(Value: ClassId.kBytecodeCid, After: ClassId.kCodeCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.1.1-0-dev.1.0-edge-pre.419+5f36c5f")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kUnresolvedClassCid, After: ClassId.kClassCid),
                new(Value: ClassId.kBoundedTypeCid, After: ClassId.kTypeParameterCid),
            ]
        ),
        [SemVersion.Parse("2.1.1-dev.0.0+a8cfe80")] = new(
            Adds: [
                new(Value: ClassId.kBoundedTypeCid, After: ClassId.kTypeParameterCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.1.1-dev.1.0-edge-10+efdbeb8")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kBoundedTypeCid, After: ClassId.kTypeParameterCid),
                new(Value: ClassId.kMixinAppTypeCid, After: ClassId.kBoundedTypeCid),
            ]
        ),
        [SemVersion.Parse("2.2.0-0-dev.2.0-edge-pre.307+7d46d4b")] = new(
            Adds: [
                new(Value: ClassId.kFfiTrampolineDataCid, After: ClassId.kRedirectionDataCid),
                new(Value: ClassId.kPointerCid, After: ClassId.kExternalTypedDataCid),
                new(Value: ClassId.kDynamicLibraryCid, After: ClassId.kPointerCid),
                new(Value: ClassId.kFfiPointerCid, After: ClassId.kExternalTwoByteStringCid),
                new(Value: ClassId.kFfiNativeFunctionCid, After: ClassId.kFfiPointerCid),
                new(Value: ClassId.kFfiInt8Cid, After: ClassId.kFfiNativeFunctionCid),
                new(Value: ClassId.kFfiInt16Cid, After: ClassId.kFfiInt8Cid),
                new(Value: ClassId.kFfiInt32Cid, After: ClassId.kFfiInt16Cid),
                new(Value: ClassId.kFfiInt64Cid, After: ClassId.kFfiInt32Cid),
                new(Value: ClassId.kFfiUint8Cid, After: ClassId.kFfiInt64Cid),
                new(Value: ClassId.kFfiUint16Cid, After: ClassId.kFfiUint8Cid),
                new(Value: ClassId.kFfiUint32Cid, After: ClassId.kFfiUint16Cid),
                new(Value: ClassId.kFfiUint64Cid, After: ClassId.kFfiUint32Cid),
                new(Value: ClassId.kFfiIntPtrCid, After: ClassId.kFfiUint64Cid),
                new(Value: ClassId.kFfiFloatCid, After: ClassId.kFfiIntPtrCid),
                new(Value: ClassId.kFfiDoubleCid, After: ClassId.kFfiFloatCid),
                new(Value: ClassId.kFfiVoidCid, After: ClassId.kFfiDoubleCid),
                new(Value: ClassId.kFfiNativeTypeCid, After: ClassId.kFfiVoidCid),
                new(Value: ClassId.kFfiDynamicLibraryCid, After: ClassId.kFfiNativeTypeCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.2.1-dev.1.0-edge-259+d445d29")] = new(
            Adds: [
                new(Value: ClassId.kTypedDataViewCid, After: ClassId.kExternalTypedDataCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.2.1-dev.2.0-edge-303+94362f1")] = new(
            Adds: [
                new(Value: ClassId.kTypedDataBaseCid, After: ClassId.kFloat64x2Cid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.2.1-dev.2.0-edge-310+a062221")] = new(
            Adds: [
                new(Value: ClassId.kTypedDataUint8ArrayCid, After: ClassId.kExternalTypedDataInt8ArrayCid),
                new(Value: ClassId.kTypedDataUint8ArrayViewCid, After: ClassId.kTypedDataUint8ArrayCid),
                new(Value: ClassId.kTypedDataUint8ClampedArrayCid, After: ClassId.kExternalTypedDataUint8ArrayCid),
                new(Value: ClassId.kTypedDataUint8ClampedArrayViewCid, After: ClassId.kTypedDataUint8ClampedArrayCid),
                new(Value: ClassId.kTypedDataInt16ArrayCid, After: ClassId.kExternalTypedDataUint8ClampedArrayCid),
                new(Value: ClassId.kTypedDataInt16ArrayViewCid, After: ClassId.kTypedDataInt16ArrayCid),
                new(Value: ClassId.kTypedDataUint16ArrayCid, After: ClassId.kExternalTypedDataInt16ArrayCid),
                new(Value: ClassId.kTypedDataUint16ArrayViewCid, After: ClassId.kTypedDataUint16ArrayCid),
                new(Value: ClassId.kTypedDataInt32ArrayCid, After: ClassId.kExternalTypedDataUint16ArrayCid),
                new(Value: ClassId.kTypedDataInt32ArrayViewCid, After: ClassId.kTypedDataInt32ArrayCid),
                new(Value: ClassId.kTypedDataUint32ArrayCid, After: ClassId.kExternalTypedDataInt32ArrayCid),
                new(Value: ClassId.kTypedDataUint32ArrayViewCid, After: ClassId.kTypedDataUint32ArrayCid),
                new(Value: ClassId.kTypedDataInt64ArrayCid, After: ClassId.kExternalTypedDataUint32ArrayCid),
                new(Value: ClassId.kTypedDataInt64ArrayViewCid, After: ClassId.kTypedDataInt64ArrayCid),
                new(Value: ClassId.kTypedDataUint64ArrayCid, After: ClassId.kExternalTypedDataInt64ArrayCid),
                new(Value: ClassId.kTypedDataUint64ArrayViewCid, After: ClassId.kTypedDataUint64ArrayCid),
                new(Value: ClassId.kTypedDataFloat32ArrayCid, After: ClassId.kExternalTypedDataUint64ArrayCid),
                new(Value: ClassId.kTypedDataFloat32ArrayViewCid, After: ClassId.kTypedDataFloat32ArrayCid),
                new(Value: ClassId.kTypedDataFloat64ArrayCid, After: ClassId.kExternalTypedDataFloat32ArrayCid),
                new(Value: ClassId.kTypedDataFloat64ArrayViewCid, After: ClassId.kTypedDataFloat64ArrayCid),
                new(Value: ClassId.kTypedDataFloat32x4ArrayCid, After: ClassId.kExternalTypedDataFloat64ArrayCid),
                new(Value: ClassId.kTypedDataFloat32x4ArrayViewCid, After: ClassId.kTypedDataFloat32x4ArrayCid),
                new(Value: ClassId.kTypedDataInt32x4ArrayCid, After: ClassId.kExternalTypedDataFloat32x4ArrayCid),
                new(Value: ClassId.kTypedDataInt32x4ArrayViewCid, After: ClassId.kTypedDataInt32x4ArrayCid),
                new(Value: ClassId.kTypedDataFloat64x2ArrayCid, After: ClassId.kExternalTypedDataInt32x4ArrayCid),
                new(Value: ClassId.kTypedDataFloat64x2ArrayViewCid, After: ClassId.kTypedDataFloat64x2ArrayCid),
                new(Value: ClassId.kByteDataViewCid, After: ClassId.kExternalTypedDataFloat64x2ArrayCid),
            ],
            Dels: [
                new(Value: ClassId.kTypedDataUint8ArrayCid, After: ClassId.kTypedDataInt8ArrayCid),
                new(Value: ClassId.kTypedDataUint8ClampedArrayCid, After: ClassId.kTypedDataUint8ArrayCid),
                new(Value: ClassId.kTypedDataInt16ArrayCid, After: ClassId.kTypedDataUint8ClampedArrayCid),
                new(Value: ClassId.kTypedDataUint16ArrayCid, After: ClassId.kTypedDataInt16ArrayCid),
                new(Value: ClassId.kTypedDataInt32ArrayCid, After: ClassId.kTypedDataUint16ArrayCid),
                new(Value: ClassId.kTypedDataUint32ArrayCid, After: ClassId.kTypedDataInt32ArrayCid),
                new(Value: ClassId.kTypedDataInt64ArrayCid, After: ClassId.kTypedDataUint32ArrayCid),
                new(Value: ClassId.kTypedDataUint64ArrayCid, After: ClassId.kTypedDataInt64ArrayCid),
                new(Value: ClassId.kTypedDataFloat32ArrayCid, After: ClassId.kTypedDataUint64ArrayCid),
                new(Value: ClassId.kTypedDataFloat64ArrayCid, After: ClassId.kTypedDataFloat32ArrayCid),
                new(Value: ClassId.kTypedDataFloat32x4ArrayCid, After: ClassId.kTypedDataFloat64ArrayCid),
                new(Value: ClassId.kTypedDataInt32x4ArrayCid, After: ClassId.kTypedDataFloat32x4ArrayCid),
                new(Value: ClassId.kTypedDataFloat64x2ArrayCid, After: ClassId.kTypedDataInt32x4ArrayCid),
                new(Value: ClassId.kTypedDataUint8ArrayViewCid, After: ClassId.kTypedDataInt8ArrayViewCid),
                new(Value: ClassId.kTypedDataUint8ClampedArrayViewCid, After: ClassId.kTypedDataUint8ArrayViewCid),
                new(Value: ClassId.kTypedDataInt16ArrayViewCid, After: ClassId.kTypedDataUint8ClampedArrayViewCid),
                new(Value: ClassId.kTypedDataUint16ArrayViewCid, After: ClassId.kTypedDataInt16ArrayViewCid),
                new(Value: ClassId.kTypedDataInt32ArrayViewCid, After: ClassId.kTypedDataUint16ArrayViewCid),
                new(Value: ClassId.kTypedDataUint32ArrayViewCid, After: ClassId.kTypedDataInt32ArrayViewCid),
                new(Value: ClassId.kTypedDataInt64ArrayViewCid, After: ClassId.kTypedDataUint32ArrayViewCid),
                new(Value: ClassId.kTypedDataUint64ArrayViewCid, After: ClassId.kTypedDataInt64ArrayViewCid),
                new(Value: ClassId.kTypedDataFloat32ArrayViewCid, After: ClassId.kTypedDataUint64ArrayViewCid),
                new(Value: ClassId.kTypedDataFloat64ArrayViewCid, After: ClassId.kTypedDataFloat32ArrayViewCid),
                new(Value: ClassId.kTypedDataFloat32x4ArrayViewCid, After: ClassId.kTypedDataFloat64ArrayViewCid),
                new(Value: ClassId.kTypedDataInt32x4ArrayViewCid, After: ClassId.kTypedDataFloat32x4ArrayViewCid),
                new(Value: ClassId.kTypedDataFloat64x2ArrayViewCid, After: ClassId.kTypedDataInt32x4ArrayViewCid),
                new(Value: ClassId.kByteDataViewCid, After: ClassId.kTypedDataFloat64x2ArrayViewCid),
            ]
        ),
        [SemVersion.Parse("2.4.0-0-dev.0.0-pre.1+b5a5d7e")] = new(
            Adds: [
                new(Value: ClassId.kTransferableTypedDataCid, After: ClassId.kUserTagCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.5.0-0-dev.0.0-edge-pre.29+9dcb026")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kTransferableTypedDataCid, After: ClassId.kUserTagCid),
            ]
        ),
        [SemVersion.Parse("2.5.0-0-dev.0.0-edge-pre.201+1d94323")] = new(
            Adds: [
                new(Value: ClassId.kParameterTypeCheckCid, After: ClassId.kContextScopeCid),
                new(Value: ClassId.kTransferableTypedDataCid, After: ClassId.kUserTagCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.5.0-0-dev.0.0-edge-pre.206+8fa1cb2")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kParameterTypeCheckCid, After: ClassId.kContextScopeCid),
            ]
        ),
        [SemVersion.Parse("2.5.0-dev.0.0-edge-271+fc6cb0a")] = new(
            Adds: [
                new(Value: ClassId.kParameterTypeCheckCid, After: ClassId.kContextScopeCid),
                new(Value: ClassId.kFfiStructCid, After: ClassId.kFfiDynamicLibraryCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.5.0-dev.2.0-edge-257+6e987b6")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kStackCid, After: ClassId.kIllegalCid),
            ]
        ),
        [SemVersion.Parse("2.6.0-dev.1.0-edge-172+7505b3a")] = new(
            Adds: [
                new(Value: ClassId.kWasmInt32Cid, After: ClassId.kFfiStructCid),
                new(Value: ClassId.kWasmInt64Cid, After: ClassId.kWasmInt32Cid),
                new(Value: ClassId.kWasmFloatCid, After: ClassId.kWasmInt64Cid),
                new(Value: ClassId.kWasmDoubleCid, After: ClassId.kWasmFloatCid),
                new(Value: ClassId.kWasmVoidCid, After: ClassId.kWasmDoubleCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.6.0-dev.7.0-edge-222+c9f6e66")] = new(
            Adds: [
                new(Value: ClassId.kCompressedStackMapsCid, After: ClassId.kCodeSourceMapCid),
            ],
            Dels: [
                new(Value: ClassId.kStackMapCid, After: ClassId.kCodeSourceMapCid),
            ]
        ),
        [SemVersion.Parse("2.7.0-0-dev.0.0-edge-pre.438+df678db")] = new(
            Adds: [
                new(Value: ClassId.kNeverCid, After: ClassId.kVoidCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.8.0-dev.2.0-edge-43+ab2026a")] = new(
            Adds: [
                new(Value: ClassId.kMonomorphicSmiableCallCid, After: ClassId.kUnlinkedCallCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.8.0-dev.3.0-edge-35+afa98b9")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kMonomorphicSmiableCallCid, After: ClassId.kUnlinkedCallCid),
            ]
        ),
        [SemVersion.Parse("2.8.0-dev.8.0-edge-91+e01457d")] = new(
            Adds: [
                new(Value: ClassId.kInstructionsSectionCid, After: ClassId.kInstructionsCid),
                new(Value: ClassId.kMonomorphicSmiableCallCid, After: ClassId.kUnlinkedCallCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.8.0-dev.9.0-edge-149+c1616f9")] = new(
            Adds: [
                new(Value: ClassId.kFutureOrCid, After: ClassId.kLinkedHashMapCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.8.0-dev.18.0-edge-94+b55342c")] = new(
            Adds: [
                new(Value: ClassId.kWeakSerializationReferenceCid, After: ClassId.kTransferableTypedDataCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.9.0-0-1.0.dev-edge-pre.118+80ae6ed")] = new(
            Adds: [
                new(Value: ClassId.kCallSiteDataCid, After: ClassId.kMonomorphicSmiableCallCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.9.0-7.0.dev-edge-32+14dfa1b")] = new(
            Adds: [
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.9.0-15.0.dev-edge-117+6544c69")] = new(
            Adds: [
                new(Value: ClassId.kFfiHandleCid, After: ClassId.kFfiVoidCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.10.0-0-0.0.dev-edge-pre.57+d4ffb92")] = new(
            Adds: [
                new(Value: ClassId.kLoadingUnitCid, After: ClassId.kSubtypeTestCacheCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.11.0-0-157.0.dev-edge-pre.16+d5649fc")] = new(
            Adds: [
                new(Value: ClassId.kImageHeaderCid, After: ClassId.kWeakSerializationReferenceCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.11.0-191.0.dev-edge-1+fca36ca")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kImageHeaderCid, After: ClassId.kWeakSerializationReferenceCid),
            ]
        ),
        [SemVersion.Parse("2.11.0-271.0.dev-edge-19+7588ed8")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kBytecodeCid, After: ClassId.kCodeCid),
                new(Value: ClassId.kParameterTypeCheckCid, After: ClassId.kContextScopeCid),
            ]
        ),
        [SemVersion.Parse("2.12.0-37.0.dev-edge-3+ade333d")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kWasmInt32Cid, After: ClassId.kFfiStructCid),
                new(Value: ClassId.kWasmInt64Cid, After: ClassId.kWasmInt32Cid),
                new(Value: ClassId.kWasmFloatCid, After: ClassId.kWasmInt64Cid),
                new(Value: ClassId.kWasmDoubleCid, After: ClassId.kWasmFloatCid),
                new(Value: ClassId.kWasmVoidCid, After: ClassId.kWasmDoubleCid),
            ]
        ),
        [SemVersion.Parse("2.12.0-38.0.dev-edge+54a91f2")] = new(
            Adds: [
                new(Value: ClassId.kWasmInt32Cid, After: ClassId.kFfiStructCid),
                new(Value: ClassId.kWasmInt64Cid, After: ClassId.kWasmInt32Cid),
                new(Value: ClassId.kWasmFloatCid, After: ClassId.kWasmInt64Cid),
                new(Value: ClassId.kWasmDoubleCid, After: ClassId.kWasmFloatCid),
                new(Value: ClassId.kWasmVoidCid, After: ClassId.kWasmDoubleCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.12.0-53.0.dev-edge+c8ebfcd")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kRedirectionDataCid, After: ClassId.kSignatureDataCid),
                new(Value: ClassId.kWasmInt32Cid, After: ClassId.kFfiStructCid),
                new(Value: ClassId.kWasmInt64Cid, After: ClassId.kWasmInt32Cid),
                new(Value: ClassId.kWasmFloatCid, After: ClassId.kWasmInt64Cid),
                new(Value: ClassId.kWasmDoubleCid, After: ClassId.kWasmFloatCid),
                new(Value: ClassId.kWasmVoidCid, After: ClassId.kWasmDoubleCid),
            ]
        ),
        [SemVersion.Parse("2.12.0-204.0.dev-edge+b3bca18")] = new(
            Adds: [
                new(Value: ClassId.kFunctionTypeCid, After: ClassId.kTypeCid),
            ],
            Dels: [
                new(Value: ClassId.kSignatureDataCid, After: ClassId.kClosureDataCid),
            ]
        ),
        [SemVersion.Parse("2.13.0-22.0.dev-edge-3+25fd020")] = new(
            Adds: [
                new(Value: ClassId.kWeakSerializationReferenceCid, After: ClassId.kKernelProgramInfoCid),
            ],
            Dels: [
                new(Value: ClassId.kWeakSerializationReferenceCid, After: ClassId.kTransferableTypedDataCid),
            ]
        ),
        [SemVersion.Parse("2.14.0-81.0.dev-edge-3+c6bffaf")] = new(
            Adds: [
                new(Value: ClassId.kInstructionsTableCid, After: ClassId.kInstructionsSectionCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.14.0-98.0.dev-edge-3+3c81d99")] = new(
            Adds: [
                new(Value: ClassId.kTypeParametersCid, After: ClassId.kFunctionCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.14.0-204.0.dev-edge+00d6b89")] = new(
            Adds: [
                new(Value: ClassId.kSentinelCid, After: ClassId.kContextScopeCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.15.0-15.0.dev-edge-6+16ff4ae")] = new(
            Adds: [
                new(Value: ClassId.kLinkedHashMapCid, After: ClassId.kTransferableTypedDataCid),
                new(Value: ClassId.kLinkedHashSetCid, After: ClassId.kLinkedHashMapCid),
            ],
            Dels: [
                new(Value: ClassId.kLinkedHashMapCid, After: ClassId.kMirrorReferenceCid),
            ]
        ),
        [SemVersion.Parse("2.15.0-20.0.dev-edge+cfb057d")] = new(
            Adds: [
                new(Value: ClassId.kGrowableObjectArrayCid, After: ClassId.kImmutableArrayCid),
            ],
            Dels: [
                new(Value: ClassId.kGrowableObjectArrayCid, After: ClassId.kBoolCid),
            ]
        ),
        [SemVersion.Parse("2.15.0-66.0.dev-edge-14+127fcf8")] = new(
            Adds: [
                new(Value: ClassId.kNativePointer, After: ClassId.kIllegalCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.15.0-82.0.dev-edge-2+e8e9e1d")] = new(
            Adds: [
                new(Value: ClassId.kImmutableLinkedHashMapCid, After: ClassId.kLinkedHashMapCid),
                new(Value: ClassId.kImmutableLinkedHashSetCid, After: ClassId.kLinkedHashSetCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.15.0-91.0.dev-edge-1+ebcf042")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kFfiPointerCid, After: ClassId.kExternalTwoByteStringCid),
                new(Value: ClassId.kFfiDynamicLibraryCid, After: ClassId.kFfiNativeTypeCid),
            ]
        ),
        [SemVersion.Parse("2.15.0-232.0.dev-edge-1+757c2b8")] = new(
            Adds: [
                new(Value: ClassId.kFfiBoolCid, After: ClassId.kFfiHandleCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.16.0-57.0.dev-edge-1+02549a8")] = new(
            Adds: [
                new(Value: ClassId.kFfiIntPtrCid, After: ClassId.kFfiDoubleCid),
            ],
            Dels: [
                new(Value: ClassId.kFfiIntPtrCid, After: ClassId.kFfiUint64Cid),
            ]
        ),
        [SemVersion.Parse("2.17.0-6.0.dev-edge+226d3c6")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kFfiIntPtrCid, After: ClassId.kFfiDoubleCid),
            ]
        ),
        [SemVersion.Parse("2.17.0-229.0.dev-edge+435ebeb")] = new(
            Adds: [
                new(Value: ClassId.kWeakReferenceCid, After: ClassId.kWeakPropertyCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.17.0-239.0.dev-edge-2+e151a81")] = new(
            Adds: [
                new(Value: ClassId.kFinalizerBaseCid, After: ClassId.kTypeCid),
                new(Value: ClassId.kFinalizerCid, After: ClassId.kFinalizerBaseCid),
                new(Value: ClassId.kFinalizerEntryCid, After: ClassId.kFinalizerCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.17.0-244.0.dev-edge+532c116")] = new(
            Adds: [
                new(Value: ClassId.kNativeFinalizerCid, After: ClassId.kFinalizerCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.18.0-68.0.dev-edge-12+bf4bb95")] = new(
            Adds: [
                new(Value: ClassId.kSuspendStateCid, After: ClassId.kStackTraceCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.19.0-81.0.dev-edge-1+d1112d3")] = new(
            Adds: [
                new(Value: ClassId.kUnmodifiableTypedDataInt8ArrayViewCid, After: ClassId.kExternalTypedDataInt8ArrayCid),
                new(Value: ClassId.kUnmodifiableTypedDataUint8ArrayViewCid, After: ClassId.kExternalTypedDataUint8ArrayCid),
                new(Value: ClassId.kUnmodifiableTypedDataUint8ClampedArrayViewCid, After: ClassId.kExternalTypedDataUint8ClampedArrayCid),
                new(Value: ClassId.kUnmodifiableTypedDataInt16ArrayViewCid, After: ClassId.kExternalTypedDataInt16ArrayCid),
                new(Value: ClassId.kUnmodifiableTypedDataUint16ArrayViewCid, After: ClassId.kExternalTypedDataUint16ArrayCid),
                new(Value: ClassId.kUnmodifiableTypedDataInt32ArrayViewCid, After: ClassId.kExternalTypedDataInt32ArrayCid),
                new(Value: ClassId.kUnmodifiableTypedDataUint32ArrayViewCid, After: ClassId.kExternalTypedDataUint32ArrayCid),
                new(Value: ClassId.kUnmodifiableTypedDataInt64ArrayViewCid, After: ClassId.kExternalTypedDataInt64ArrayCid),
                new(Value: ClassId.kUnmodifiableTypedDataUint64ArrayViewCid, After: ClassId.kExternalTypedDataUint64ArrayCid),
                new(Value: ClassId.kUnmodifiableTypedDataFloat32ArrayViewCid, After: ClassId.kExternalTypedDataFloat32ArrayCid),
                new(Value: ClassId.kUnmodifiableTypedDataFloat64ArrayViewCid, After: ClassId.kExternalTypedDataFloat64ArrayCid),
                new(Value: ClassId.kUnmodifiableTypedDataFloat32x4ArrayViewCid, After: ClassId.kExternalTypedDataFloat32x4ArrayCid),
                new(Value: ClassId.kUnmodifiableTypedDataInt32x4ArrayViewCid, After: ClassId.kExternalTypedDataInt32x4ArrayCid),
                new(Value: ClassId.kUnmodifiableTypedDataFloat64x2ArrayViewCid, After: ClassId.kExternalTypedDataFloat64x2ArrayCid),
                new(Value: ClassId.kUnmodifiableByteDataViewCid, After: ClassId.kByteDataViewCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.19.0-192.0.dev-edge-1+9a023ae")] = new(
            Adds: [
                new(Value: ClassId.kFunctionTypeCid, After: ClassId.kTypeCid),
                new(Value: ClassId.kRecordTypeCid, After: ClassId.kFunctionTypeCid),
                new(Value: ClassId.kTypeRefCid, After: ClassId.kRecordTypeCid),
                new(Value: ClassId.kTypeParameterCid, After: ClassId.kTypeRefCid),
            ],
            Dels: [
                new(Value: ClassId.kFunctionTypeCid, After: ClassId.kFinalizerEntryCid),
                new(Value: ClassId.kTypeRefCid, After: ClassId.kFunctionTypeCid),
                new(Value: ClassId.kTypeParameterCid, After: ClassId.kTypeRefCid),
            ]
        ),
        [SemVersion.Parse("2.19.0-192.0.dev-edge-2+c94103a")] = new(
            Adds: [
                new(Value: ClassId.kRecordCid, After: ClassId.kFloat64x2Cid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("2.19.0-404.0.dev-edge+a2de36e")] = new(
            Adds: [
                new(Value: ClassId.kMapCid, After: ClassId.kTransferableTypedDataCid),
                new(Value: ClassId.kConstMapCid, After: ClassId.kMapCid),
                new(Value: ClassId.kSetCid, After: ClassId.kConstMapCid),
                new(Value: ClassId.kConstSetCid, After: ClassId.kSetCid),
            ],
            Dels: [
                new(Value: ClassId.kLinkedHashMapCid, After: ClassId.kTransferableTypedDataCid),
                new(Value: ClassId.kImmutableLinkedHashMapCid, After: ClassId.kLinkedHashMapCid),
                new(Value: ClassId.kLinkedHashSetCid, After: ClassId.kImmutableLinkedHashMapCid),
                new(Value: ClassId.kImmutableLinkedHashSetCid, After: ClassId.kLinkedHashSetCid),
            ]
        ),
        [SemVersion.Parse("3.0.0-133.0.dev-edge-6+f3067e8")] = new(
            Adds: [
                new(Value: ClassId.kWeakArrayCid, After: ClassId.kWeakSerializationReferenceCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("3.1.0-56.0.dev-edge-1+2ee6fcf")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kTypeRefCid, After: ClassId.kRecordTypeCid),
            ]
        ),
        [SemVersion.Parse("3.4.0-253.0.dev-edge-4+17d6ba1")] = new(
            Adds: [
            ],
            Dels: [
                new(Value: ClassId.kExternalOneByteStringCid, After: ClassId.kTwoByteStringCid),
                new(Value: ClassId.kExternalTwoByteStringCid, After: ClassId.kExternalOneByteStringCid),
            ]
        ),
        [SemVersion.Parse("3.6.0-149.0.dev-edge-28+8fbca8b")] = new(
            Adds: [
                new(Value: ClassId.kBytecodeCid, After: ClassId.kCodeCid),
            ],
            Dels: [
            ]
        ),
        [SemVersion.Parse("3.9.0-47.0.dev-edge+5559d1e")] = new(
            Adds: [
                new(Value: ClassId.kUnlinkedCallCid, After: ClassId.kCallSiteDataCid),
            ],
            Dels: [
                new(Value: ClassId.kUnlinkedCallCid, After: ClassId.kSingleTargetCacheCid),
            ]
        ),
    };
}
