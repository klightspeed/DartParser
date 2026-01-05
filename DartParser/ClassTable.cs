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

namespace DartParser
{
    public class ClassTable : Dictionary<ClassId, DartClass>
    {
        private string? SnapshotVersion;
        private SemVersion? Version;
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


        private readonly record struct ValueAfter<T>(T Value, T After) where T : notnull;
        private readonly record struct AddsDels<T>(List<ValueAfter<T>> Adds, List<ValueAfter<T>> Dels) where T : notnull;
        private delegate int FieldSizeDelegate(SnapshotKind kind, bool isProduct);

        private static readonly Dictionary<SemVersion, AddsDels<ClassId>> ClassIdChanges = new()
        {
            [SemVersion.Parse("1.11.0-dev.0.0-edge-24+ad130e2")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ClassId.kDeoptInfoCid, After: ClassId.kExceptionHandlersCid),
                ]
            ),
            [SemVersion.Parse("1.12.0-dev.0.0-edge-15+69938db")] = new(
                Adds: [
                    new(Value: ClassId.kObjectPoolCid, After: ClassId.kInstructionsCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("1.14.0-dev.7.0-edge-167+bbee77d")] = new(
                Adds: [
                    new(Value: ClassId.kFunctionTypeCid, After: ClassId.kTypeCid),
                    new(Value: ClassId.kClosureCid, After: ClassId.kMixinAppTypeCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("1.15.0-dev.0.0-edge-99+d77d376")] = new(
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
            [SemVersion.Parse("1.16.0-dev.4.0-edge-111+cb63ed4")] = new(
                Adds: [
                    new(Value: ClassId.kRegExpCid, After: ClassId.kStacktraceCid),
                ],
                Dels: [
                    new(Value: ClassId.kJSRegExpCid, After: ClassId.kStacktraceCid),
                ]
            ),
            [SemVersion.Parse("1.19.0-dev.1.0-edge-9+71c2404")] = new(
                Adds: [
                    new(Value: ClassId.kForwardingCorpse, After: ClassId.kFreeListElement),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("1.20.0-dev.1.0-edge-1207+3aa65a6")] = new(
                Adds: [
                    new(Value: ClassId.kSingleTargetCacheCid, After: ClassId.kContextScopeCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("1.20.0-dev.3.0-edge-60+6137d02")] = new(
                Adds: [
                    new(Value: ClassId.kFreeListElement, After: ClassId.kIllegalCid),
                    new(Value: ClassId.kForwardingCorpse, After: ClassId.kFreeListElement),
                ],
                Dels: [
                    new(Value: ClassId.kFreeListElement, After: ClassId.kVoidCid),
                    new(Value: ClassId.kForwardingCorpse, After: ClassId.kFreeListElement),
                ]
            ),
            [SemVersion.Parse("1.20.0-dev.10.0-edge-33+6cff17c")] = new(
                Adds: [
                    new(Value: ClassId.kUnlinkedCallCid, After: ClassId.kSingleTargetCacheCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("1.21.0-dev.11.0-edge-138+c9399fc")] = new(
                Adds: [
                    new(Value: ClassId.kStackCid, After: ClassId.kIllegalCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("1.22.0-dev.1.0-edge-115+dc426c8")] = new(
                Adds: [
                    new(Value: ClassId.kStackMapCid, After: ClassId.kCodeSourceMapCid),
                    new(Value: ClassId.kStackTraceCid, After: ClassId.kSendPortCid),
                ],
                Dels: [
                    new(Value: ClassId.kStackmapCid, After: ClassId.kCodeSourceMapCid),
                    new(Value: ClassId.kStacktraceCid, After: ClassId.kSendPortCid),
                ]
            ),
            [SemVersion.Parse("1.22.0-dev.10.0-edge-436+bee82fe")] = new(
                Adds: [
                    new(Value: ClassId.kSignatureDataCid, After: ClassId.kClosureDataCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("1.25.0-dev.14.0-edge-9+7bf835b")] = new(
                Adds: [
                    new(Value: ClassId.kVectorCid, After: ClassId.kVoidCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.0.0-dev.1.0-edge-201+5efa08b")] = new(
                Adds: [
                    new(Value: ClassId.kTypeArgumentsCid, After: ClassId.kLibraryPrefixCid),
                ],
                Dels: [
                    new(Value: ClassId.kTypeArgumentsCid, After: ClassId.kUnresolvedClassCid),
                ]
            ),
            [SemVersion.Parse("2.0.0-dev.40.0-edge-76+95e9e89")] = new(
                Adds: [
                    new(Value: ClassId.kKernelProgramInfoCid, After: ClassId.kNamespaceCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.0.0-dev.58.0-edge-56+ec19ebd")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ClassId.kVectorCid, After: ClassId.kVoidCid),
                ]
            ),
            [SemVersion.Parse("2.0.0-dev.68.0-edge-31+3e59362")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ClassId.kBigintCid, After: ClassId.kMintCid),
                ]
            ),
            [SemVersion.Parse("2.1.0-dev.3.0-edge-104+dbe868d")] = new(
                Adds: [
                    new(Value: ClassId.kNativeEntryDataCid, After: ClassId.kRedirectionDataCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.1.0-dev.7.0-edge-353+d91e0ad")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ClassId.kNativeEntryDataCid, After: ClassId.kRedirectionDataCid),
                    new(Value: ClassId.kLiteralTokenCid, After: ClassId.kFieldCid),
                    new(Value: ClassId.kTokenStreamCid, After: ClassId.kLiteralTokenCid),
                ]
            ),
            [SemVersion.Parse("2.1.0-dev.8.0-edge-393+2535b1e")] = new(
                Adds: [
                    new(Value: ClassId.kBytecodeCid, After: ClassId.kCodeCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.1.1-dev.2.0-edge-246+2776000")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ClassId.kUnresolvedClassCid, After: ClassId.kClassCid),
                    new(Value: ClassId.kBoundedTypeCid, After: ClassId.kTypeParameterCid),
                    new(Value: ClassId.kMixinAppTypeCid, After: ClassId.kBoundedTypeCid),
                ]
            ),
            [SemVersion.Parse("2.2.0-dev.1.1-1+a8cfe80")] = new(
                Adds: [
                    new(Value: ClassId.kBoundedTypeCid, After: ClassId.kTypeParameterCid),
                    new(Value: ClassId.kMixinAppTypeCid, After: ClassId.kBoundedTypeCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.2.0-dev.1.1-edge-488+e4e77b9")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ClassId.kBoundedTypeCid, After: ClassId.kTypeParameterCid),
                ]
            ),
            [SemVersion.Parse("2.2.0-dev.2.0-edge-181+ee32f8c")] = new(
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
                    new(Value: ClassId.kMixinAppTypeCid, After: ClassId.kTypeParameterCid),
                ]
            ),
            [SemVersion.Parse("2.2.1-dev.2.0-edge-212+61f0f5b")] = new(
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
            [SemVersion.Parse("2.3.1-dev.0.0-edge-22+6fe73e4")] = new(
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
            [SemVersion.Parse("2.3.2-dev.0.1-edge-158+5167ec9")] = new(
                Adds: [
                    new(Value: ClassId.kTransferableTypedDataCid, After: ClassId.kUserTagCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.5.0-dev.0.0-edge-33+929c45a")] = new(
                Adds: [
                    new(Value: ClassId.kParameterTypeCheckCid, After: ClassId.kContextScopeCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.5.0-dev.1.0-edge-616+d08c203")] = new(
                Adds: [
                    new(Value: ClassId.kFfiStructCid, After: ClassId.kFfiDynamicLibraryCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.5.0-dev.4.0-edge-441+7c8c197")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ClassId.kStackCid, After: ClassId.kIllegalCid),
                ]
            ),
            [SemVersion.Parse("2.6.0-dev.2.0-edge-138+e6574a7")] = new(
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
            [SemVersion.Parse("2.6.0-dev.8.0-edge-121+aa569df")] = new(
                Adds: [
                    new(Value: ClassId.kCompressedStackMapsCid, After: ClassId.kCodeSourceMapCid),
                ],
                Dels: [
                    new(Value: ClassId.kStackMapCid, After: ClassId.kCodeSourceMapCid),
                ]
            ),
            [SemVersion.Parse("2.8.0-dev.0.0-edge-598+8a39cb8")] = new(
                Adds: [
                    new(Value: ClassId.kNeverCid, After: ClassId.kVoidCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.8.0-dev.7.0-edge-3+d3b7909")] = new(
                Adds: [
                    new(Value: ClassId.kMonomorphicSmiableCallCid, After: ClassId.kUnlinkedCallCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.8.0-dev.9.0-edge-89+6a7d4e2")] = new(
                Adds: [
                    new(Value: ClassId.kInstructionsSectionCid, After: ClassId.kInstructionsCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.8.0-dev.16.0-edge-5+a94fb21")] = new(
                Adds: [
                    new(Value: ClassId.kFutureOrCid, After: ClassId.kLinkedHashMapCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.8.0-dev.20.0-edge-11+74da2ed")] = new(
                Adds: [
                    new(Value: ClassId.kWeakSerializationReferenceCid, After: ClassId.kTransferableTypedDataCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.9.0-5.0.dev-edge-40+77a4964")] = new(
                Adds: [
                    new(Value: ClassId.kCallSiteDataCid, After: ClassId.kMonomorphicSmiableCallCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.9.0-9.0.dev-edge-47+14103a3")] = new(
                Adds: [
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.9.0-21.4.beta-3+d6c87ec")] = new(
                Adds: [
                    new(Value: ClassId.kFfiHandleCid, After: ClassId.kFfiVoidCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.10.0-83.0.dev-edge-6+c1bb9a5")] = new(
                Adds: [
                    new(Value: ClassId.kLoadingUnitCid, After: ClassId.kSubtypeTestCacheCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.11.0-159.0.dev-edge-7+a36d177")] = new(
                Adds: [
                    new(Value: ClassId.kImageHeaderCid, After: ClassId.kWeakSerializationReferenceCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.12.0-0.0.dev-edge-2+2259593")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ClassId.kBytecodeCid, After: ClassId.kCodeCid),
                    new(Value: ClassId.kParameterTypeCheckCid, After: ClassId.kContextScopeCid),
                    new(Value: ClassId.kImageHeaderCid, After: ClassId.kWeakSerializationReferenceCid),
                ]
            ),
            [SemVersion.Parse("2.12.0-37.0.dev-edge-17+fadf8fd")] = new(
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
            [SemVersion.Parse("2.12.0-112.0.dev-edge-1+b9fc385")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ClassId.kRedirectionDataCid, After: ClassId.kSignatureDataCid),
                ]
            ),
            [SemVersion.Parse("2.12.0-239.0.dev-edge-1+f8b0d26")] = new(
                Adds: [
                    new(Value: ClassId.kFunctionTypeCid, After: ClassId.kTypeCid),
                ],
                Dels: [
                    new(Value: ClassId.kSignatureDataCid, After: ClassId.kClosureDataCid),
                ]
            ),
            [SemVersion.Parse("2.13.0-25.0.dev-edge-19+18f0599")] = new(
                Adds: [
                    new(Value: ClassId.kWeakSerializationReferenceCid, After: ClassId.kKernelProgramInfoCid),
                ],
                Dels: [
                    new(Value: ClassId.kWeakSerializationReferenceCid, After: ClassId.kTransferableTypedDataCid),
                ]
            ),
            [SemVersion.Parse("2.14.0-31.0.dev-edge-3+ab9ccf1")] = new(
                Adds: [
                    new(Value: ClassId.kInstructionsTableCid, After: ClassId.kInstructionsSectionCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.14.0-111.0.dev-edge-2+64cf957")] = new(
                Adds: [
                    new(Value: ClassId.kTypeParametersCid, After: ClassId.kFunctionCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.14.0-257.0.dev-edge-4+130fa82")] = new(
                Adds: [
                    new(Value: ClassId.kSentinelCid, After: ClassId.kContextScopeCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.14.0-347.0.dev-edge-2+34724ec")] = new(
                Adds: [
                    new(Value: ClassId.kLinkedHashMapCid, After: ClassId.kTransferableTypedDataCid),
                    new(Value: ClassId.kLinkedHashSetCid, After: ClassId.kLinkedHashMapCid),
                ],
                Dels: [
                    new(Value: ClassId.kLinkedHashMapCid, After: ClassId.kMirrorReferenceCid),
                ]
            ),
            [SemVersion.Parse("2.15.0-51.0.dev-edge-2+a81945e")] = new(
                Adds: [
                    new(Value: ClassId.kGrowableObjectArrayCid, After: ClassId.kImmutableArrayCid),
                ],
                Dels: [
                    new(Value: ClassId.kGrowableObjectArrayCid, After: ClassId.kBoolCid),
                ]
            ),
            [SemVersion.Parse("2.15.0-69.0.dev-edge-8+4bd4de0")] = new(
                Adds: [
                    new(Value: ClassId.kNativePointer, After: ClassId.kIllegalCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.15.0-90.0.dev-edge-22+77467fe")] = new(
                Adds: [
                    new(Value: ClassId.kImmutableLinkedHashMapCid, After: ClassId.kLinkedHashMapCid),
                    new(Value: ClassId.kImmutableLinkedHashSetCid, After: ClassId.kLinkedHashSetCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.15.0-138.0.dev-edge-12+9854a04")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ClassId.kFfiPointerCid, After: ClassId.kExternalTwoByteStringCid),
                    new(Value: ClassId.kFfiDynamicLibraryCid, After: ClassId.kFfiNativeTypeCid),
                ]
            ),
            [SemVersion.Parse("2.15.0-298.0.dev-edge+304330b")] = new(
                Adds: [
                    new(Value: ClassId.kFfiBoolCid, After: ClassId.kFfiHandleCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.16.0-106.0.dev-edge+ecdf148")] = new(
                Adds: [
                    new(Value: ClassId.kFfiIntPtrCid, After: ClassId.kFfiDoubleCid),
                ],
                Dels: [
                    new(Value: ClassId.kFfiIntPtrCid, After: ClassId.kFfiUint64Cid),
                ]
            ),
            [SemVersion.Parse("2.17.0-34.0.dev-edge-2+4088cd0")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ClassId.kFfiIntPtrCid, After: ClassId.kFfiDoubleCid),
                ]
            ),
            [SemVersion.Parse("2.17.0-97.0.dev-edge-18+828dcd0")] = new(
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
            [SemVersion.Parse("2.18.0-68.0.dev-edge-4+6f24cbc")] = new(
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
            [SemVersion.Parse("2.19.0-183.0.dev-edge-7+79afcf9")] = new(
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
            [SemVersion.Parse("2.19.0-359.0.dev-edge-3+82dc3cb")] = new(
                Adds: [
                    new(Value: ClassId.kRecordCid, After: ClassId.kFloat64x2Cid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("3.0.0-105.0.dev-edge+88400bd")] = new(
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
            [SemVersion.Parse("3.0.0-382.0.dev-edge-4+ed09412")] = new(
                Adds: [
                    new(Value: ClassId.kWeakArrayCid, After: ClassId.kWeakSerializationReferenceCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("3.1.0-200.0.dev-edge-2+dfce3aa")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ClassId.kTypeRefCid, After: ClassId.kRecordTypeCid),
                ]
            ),
            [SemVersion.Parse("3.4.0-268.0.dev-edge+35d8630")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ClassId.kExternalOneByteStringCid, After: ClassId.kTwoByteStringCid),
                    new(Value: ClassId.kExternalTwoByteStringCid, After: ClassId.kExternalOneByteStringCid),
                ]
            ),
            [SemVersion.Parse("3.7.0-226.0.dev-edge-1+21db850")] = new(
                Adds: [
                    new(Value: ClassId.kBytecodeCid, After: ClassId.kCodeCid),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("3.9.0-82.0.dev-edge-2+16909d8")] = new(
                Adds: [
                    new(Value: ClassId.kUnlinkedCallCid, After: ClassId.kCallSiteDataCid),
                ],
                Dels: [
                    new(Value: ClassId.kUnlinkedCallCid, After: ClassId.kSingleTargetCacheCid),
                ]
            ),

        };

        private static readonly Dictionary<SemVersion, AddsDels<Func<ClassTable, DartObject>>> BaseObjectChanges = new()
        {
            [SemVersion.Parse("3.7.0-226.0.dev-edge-1+21db850")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ct => ct.TransitionSentinel, After: ct => ct.Sentinel)
                ]
            ),
            [SemVersion.Parse("3.4.0-268.0.dev-edge+35d8630")] = new(
                Adds: [
                    new(Value: ct => ct.EmptySubtypeTestCacheArray, After: ct => ct.EmptyInstantiationsCacheArray)
                ],
                Dels: [
                    new(Value: ct => ct.EmptySubtypeTestCacheArray, After: ct => ct.CachedICDataArrays[^1])
                ]
            ),
            [SemVersion.Parse("3.1.0-200.0.dev-edge-2+dfce3aa")] = new(
                Adds: [
                    new(Value: ct => ct.OptimizedOut, After: ct => ct.TransitionSentinel)
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("3.0.0-105.0.dev-edge+88400bd")] = new(
                Adds: [
                    new(Value: ct => ct.EmptyInstantiationsCacheArray, After: ct => ct.EmptyArray)
                ],
                Dels: [
                    new(Value: ct => ct.ZeroArray, After: ct => ct.EmptyArray)
                ]
            ),
            [SemVersion.Parse("2.19.0-359.0.dev-edge-3+82dc3cb")] = new(
                Adds: [
                    new(Value: ct => ct.SyntheticGetterParameterTypes, After: ct => ct.False),
                    new(Value: ct => ct.SyntheticGetterParameterNames, After: ct => ct.SyntheticGetterParameterTypes),
                ],
                Dels: [
                    new(Value: ct => ct.ExtractorParameterTypes, After: ct => ct.False),
                    new(Value: ct => ct.ExtractorParameterNames, After: ct => ct.ExtractorParameterTypes),
                ]
            ),
            [SemVersion.Parse("2.18.0-68.0.dev-edge-12+bf4bb95")] = new(
                Adds: [
                    new(Value: ct => ct.EmptyAsyncExceptionHandlers, After: ct => ct.EmptyExceptionHandlers)
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.12.0-112.0.dev-edge-1+b9fc385")] = new(
                Adds: [
                    new(Value: ct => ct.EmptyObjectPool, After: ct => ct.EmptyContextScope),
                    new(Value: ct => ct.EmptyCompressedStackMaps, After: ct => ct.EmptyObjectPool),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.15.0-13.0.dev-edge-1+e8ddc02")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ct => ct.ImplicitGetterBytecode, After: ct => ct.EmptyExceptionHandlers),
                    new(Value: ct => ct.ImplicitSetterBytecode, After: ct => ct.ImplicitGetterBytecode),
                    new(Value: ct => ct.ImplicitStaticGetterBytecode, After: ct => ct.ImplicitSetterBytecode),
                    new(Value: ct => ct.MethodExtractorBytecode, After: ct => ct.ImplicitStaticGetterBytecode),
                    new(Value: ct => ct.InvokeClosureBytecode, After: ct => ct.MethodExtractorBytecode),
                    new(Value: ct => ct.InvokeFieldBytecode, After: ct => ct.InvokeClosureBytecode),
                    new(Value: ct => ct.NsmDispatcherBytecode, After: ct => ct.InvokeFieldBytecode),
                    new(Value: ct => ct.DynamicInvocationForwarderBytecode, After: ct => ct.NsmDispatcherBytecode),
                ]
            ),
            [SemVersion.Parse("2.9.0-21.0.dev-edge-239+2a9516a")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ct => ct.NeverType, After: ct => ct.VoidType),
                    new(Value: ct => ct[ClassId.kNeverCid], After: ct => ct[ClassId.kVoidCid]),
                ]
            ),
            [SemVersion.Parse("2.8.0-dev.9.0-edge-89+6a7d4e2")] = new(
                Adds: [
                    new(Value: ct => ct.NeverType, After: ct => ct.VoidType),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.8.0-dev.0.0-edge-598+8a39cb8")] = new(
                Adds: [
                    new(Value: ct => ct[ClassId.kNeverCid], After: ct => ct[ClassId.kVoidCid]),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.5.0-dev.0.0-edge-33+929c45a")] = new(
                Adds: [
                    new(Value: ct => ct.DynamicInvocationForwarderBytecode, After: ct => ct.NsmDispatcherBytecode),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.3.1-dev.0.0-edge-22+6fe73e4")] = new(
                Adds: [
                    new(Value: ct => ct.NsmDispatcherBytecode, After: ct => ct.InvokeFieldBytecode),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.3.0-dev.0.0-edge-105+86fe7ca")] = new(
                Adds: [
                    new(Value: ct => ct.InvokeFieldBytecode, After: ct => ct.InvokeClosureBytecode),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.3.0-dev.0.0-edge-48+503e209")] = new(
                Adds: [
                    new(Value: ct => ct.InvokeClosureBytecode, After: ct => ct.MethodExtractorBytecode),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.3.0-dev.0.0-edge-18+38049f3")] = new(
                Adds: [
                    new(Value: ct => ct.ImplicitStaticGetterBytecode, After: ct => ct.ImplicitSetterBytecode),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.2.1-dev.4.0-edge-83+ce9a1e2")] = new(
                Adds: [
                    new(Value: ct => ct.MethodExtractorBytecode, After: ct => ct.ImplicitSetterBytecode),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.2.1-dev.2.0-edge-212+61f0f5b")] = new(
                Adds: [
                    new(Value: ct => ct.ImplicitGetterBytecode, After: ct => ct.EmptyExceptionHandlers),
                    new(Value: ct => ct.ImplicitSetterBytecode, After: ct => ct.ImplicitGetterBytecode),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.1.0-dev.7.0-edge-353+d91e0ad")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ct => ct.EmptyContext, After: ct => ct.ExtractorParameterNames),
                ]
            ),
            [SemVersion.Parse("2.0.0-dev.49.0-edge-18+7e64ddd")] = new(
                Adds: [
                    new(Value: ct => ct.EmptyTypeArguments, After: ct => ct.VoidType),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("2.0.0-dev.18.0-edge-36+9c40a7e")] = new(
                Adds: [
                ],
                Dels: [
                    new(Value: ct => ct.EmptyTypeArguments, After: ct => ct.TransitionSentinel),
                ]
            ),
            [SemVersion.Parse("1.23.0-dev.11.0-edge-478+c4983a5")] = new(
                Adds: [
                    new(Value: ct => ct.EmptyTypeArguments, After: ct => ct.TransitionSentinel),
                ],
                Dels: [
                ]
            ),
            [SemVersion.Parse("1.23.0-dev.11.0-edge-267+d0a7bad")] = new(
                Adds: [
                    new(Value: ct => ct.EmptyContext, After: ct => ct.ExtractorParameterNames),
                ],
                Dels: [
                ]
            ),


        };

        #region Objects
        public DartObject Null => DartObject.Null;

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

        public DartBool True => DartBool.True;

        public DartBool False => DartBool.False;

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
                    Data = [.. Enumerable.Repeat(DartObject.Null, n)]
                }
            )
        ];

        public ImmutableArray<DartArray> CachedICDataArrays_Pre221 { get; } = [
            .. Enumerable.Range(0, 4).Select(n =>
                new DartArray(ClassId.kICDataCid)
                {
                    Description = $"<empty icdata entries[{n}]>",
                    Length = (ulong)n,
                    Data = [.. Enumerable.Repeat(DartObject.Null, n)]
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

                ClassIdLookup = InitClassIdLookup(snapshot.Version);

                foreach (var classid in ClassIdLookup)
                {
                    this[classid] = new DartClass(classid);
                }

                BaseObjects = InitBaseObjects(snapshot);
            }

            if (snapshot.Objects.Count == 0)
            {
                snapshot.Objects.AddRange(BaseObjects);
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

            foreach (var (changeVer, (adds, dels)) in BaseObjectChanges.AsEnumerable().Reverse())
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

            foreach (var (changeVer, (adds, dels)) in ClassIdChanges.AsEnumerable().Reverse())
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

        public static ClassTable CloneFromVMIsolate(DartIsolate vmIsolate)
        {
            return new ClassTable(vmIsolate.ClassTable, vmIsolate.Objects);
        }
    }
}
