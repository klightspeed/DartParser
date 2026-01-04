using DartParser.Dart.Objects;

namespace DartParser.Dart.Objects.ToCheck;

public class DartTypeParameter : DartAbstractType
{
    [DartField]
    [LastSnapshotFieldFor(All = true)]
    public DartObject? Owner { get; set; }
    
    public ushort Base { get; set; }

    public ushort Index { get; set; }
}
