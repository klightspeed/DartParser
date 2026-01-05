namespace DartParser;

[AttributeUsage(AttributeTargets.Property)]
public class DartVersionRangeAttribute(string versionRange) : Attribute
{
    public string VersionRange { get; set; } = versionRange;
}
