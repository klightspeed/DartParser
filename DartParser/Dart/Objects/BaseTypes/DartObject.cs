using DartParser.Dart.Objects.Other;
using DartParser.Dart.Objects.Singletons;
using Semver;
using System.Diagnostics;

namespace DartParser.Dart.Objects.BaseTypes;

[DebuggerDisplay("{Type} {Description}")]
public class DartObject : IHasObjectLayout<DartObject>
{
    public int ObjectIndex { get; set; }
    public DartClass? Class { get; set; }
    public virtual string? Description { get; init; }
    public ClassId Type { get; set; }

    public List<(DartObject Link, string PropertyName)> LinkedObjects { get; } = [];

    public DartObject(ClassId type)
    {
        this.Description = type.ToString();
        this.Type = type;
    }

    public DartObject(string description, ClassId type)
    {
        this.Description = description;
        this.Type = type;
    }

    public static void InitObjectLayout(DartObjectLayout<DartObject> setters, SemVersion version, SnapshotKind kind, bool isProduct, bool is64Bit)
    {
    }
}
