using DartParser.Dart;
using DartParser.Dart.Objects;

namespace DartParser.Dart.Objects.ToCheck;

public class DartCallSiteData(ClassId cid) : DartObject(cid)
{
    public DartCallSiteData() : this(ClassId.kCallSiteDataCid) { }

    [DartField]
    public DartString? TargetName { get; set; }

    [DartField]
    [LastSnapshotFieldFor(All = true)]
    public DartArray? ArgsDescriptor { get; set; }
}
