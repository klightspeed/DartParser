namespace DartParser;

[AttributeUsage(AttributeTargets.Property)]
public class OnlyInAttribute(SnapshotKind kind) : Attribute
{
    public SnapshotKind Kind { get; set; } = kind;
}
