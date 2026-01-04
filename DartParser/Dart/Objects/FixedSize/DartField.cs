using DartParser.Dart;
using DartParser.Dart.Objects;
using DartParser.Dart.Objects.ToCheck;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartField() : DartObject(ClassId.kFieldCid), IDartObject<DartField>
{
    public DartString? Name { get; set; }
    public DartObject? Owner { get; set; }
    public DartAbstractType? FieldType { get; set; }
    public DartFunction? InitializerFunction { get; set; }
    public DartInteger? HostOffsetOrFieldId { get; set; }
    public DartInteger? GuardedListLength { get; set; }
    public DartAbstractType? ExactType { get; set; }
    public DartWeakArray? DependentCode { get; set; }

    public ulong TokenPosition { get; set; }
    public ulong EndTokenPosition { get; set; }
    public ClassId GuardedCid { get; set; }
    public ClassId IsNullable { get; set; }
    public ulong KernelOffset { get; set; }
    public ulong GuardedListLengthInObjectOffset { get; set; }
    public byte StaticTypeExactnessState { get; set; }
    public ulong KindBits { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartField> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.Name);
        setters.AddRef(e => e.Owner);
        setters.AddRef(e => e.FieldType);
        setters.AddRef(e => e.InitializerFunction);

        if (kind != SnapshotKind.kFullAOT)
        {
            setters.AddRef(e => e.GuardedListLength);

            if (kind == SnapshotKind.kFullJIT)
            {
                setters.AddRef(e => e.DependentCode);
            }

            setters.AddValue(e => e.TokenPosition);
            setters.AddValue(e => e.EndTokenPosition);
            setters.AddCid(e => e.GuardedCid);
            setters.AddCid(e => e.IsNullable);
            setters.AddValue(e => e.StaticTypeExactnessState);
            setters.AddValue(e => e.KernelOffset);
        }

        setters.AddValue(e => e.KindBits);
        setters.AddRef(e => e.HostOffsetOrFieldId);
    }
}
