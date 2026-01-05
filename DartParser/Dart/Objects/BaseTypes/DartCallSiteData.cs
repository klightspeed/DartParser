using DartParser.Dart.Objects.Canonical;
using DartParser.Dart.Objects.VariableLength;

namespace DartParser.Dart.Objects.BaseTypes;

public class DartCallSiteData(ClassId cid) : DartObject(cid)
{
    public DartCallSiteData() : this(ClassId.kCallSiteDataCid) { }
    public DartString? TargetName { get; set; }
    public DartArray? ArgsDescriptor { get; set; }
}
