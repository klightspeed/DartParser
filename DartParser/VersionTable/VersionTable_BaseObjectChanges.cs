using DartParser.Dart;
using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser;

public static partial class VersionTable
{
    public static readonly Dictionary<SemVersion, AddsDels<Func<ClassTable, DartObject>>> BaseObjectChanges = new()
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
}
