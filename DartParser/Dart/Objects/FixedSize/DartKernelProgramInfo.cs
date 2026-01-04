using DartParser.Dart;
using DartParser.Dart.Objects;
using DartParser.Dart.Objects.ToCheck;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartKernelProgramInfo() : DartObject(ClassId.kKernelProgramInfoCid), IDartObject<DartKernelProgramInfo>
{
    public DartTypedData? KernelComponent { get; set; }
    public DartTypedData? StringOffsets { get; set; }
    public DartTypedData? StringData { get; set; }
    public DartTypedData? CanonicalNames { get; set; }
    public DartTypedData? MetadataPayloads { get; set; }
    public DartTypedData? MetadataMappings { get; set; }
    public DartArray? Scripts { get; set; }
    public DartArray? Constants { get; set; }
    public DartTypedData? ConstantsTable { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartKernelProgramInfo> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.KernelComponent);
        setters.AddRef(e => e.StringOffsets);
        setters.AddRef(e => e.StringData);
        setters.AddRef(e => e.CanonicalNames);
        setters.AddRef(e => e.MetadataPayloads);
        setters.AddRef(e => e.MetadataMappings);
        setters.AddRef(e => e.Scripts);
        setters.AddRef(e => e.Constants);
        setters.AddRef(e => e.ConstantsTable);
    }
}
