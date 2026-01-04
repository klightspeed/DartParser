namespace DartParser.Dart.Objects.ToCheck;

public class DartType : DartAbstractType
{
    [DartField]
    [LastSnapshotFieldFor(All = true)]
    public DartTypeArguments? Arguments { get; set; }
}
