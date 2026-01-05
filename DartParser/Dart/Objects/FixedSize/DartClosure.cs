using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Canonical;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartClosure() : DartInstance(ClassId.kClosureCid), IHasPropertySetters<DartClosure>
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
