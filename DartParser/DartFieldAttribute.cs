namespace DartParser;

[AttributeUsage(AttributeTargets.Property)]
public class DartFieldAttribute : Attribute
{
    public bool NotInProduct { get; set; }
    public bool NotInAOT { get; set; }
    public bool NotInAOTProduct { get; set; }
    public bool OnlyInProduct { get; set; }
    public bool OnlyInAOT { get; set; }
    public bool OnlyInAOTProduct { get; set; }
}
