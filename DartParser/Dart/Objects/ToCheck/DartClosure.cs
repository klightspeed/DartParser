using DartParser.Dart;
using DartParser.Dart.Objects;
using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.FixedSize;
using Semver;

namespace DartParser.Dart.Objects.ToCheck;

public class DartClosure() : DartInstance(ClassId.kClosureCid), IDartObject<DartClosure>
{
    public DartTypeArguments? InstantiatorTypeArguments { get; set; }
    public DartTypeArguments? FunctionTypeArguments { get; set; }
    public DartTypeArguments? DelayedTypeArguments { get; set; }
    public DartFunction? Function { get; set; }
    public DartObject? Context { get; set; }
    public DartObject? Hash { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartClosure> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.InstantiatorTypeArguments);
        setters.AddRef(e => e.FunctionTypeArguments);
        setters.AddRef(e => e.DelayedTypeArguments);
        setters.AddRef(e => e.Function);
        setters.AddRef(e => e.Context);
        setters.AddRef(e => e.Hash);
    }
}
