using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartTypedDataView(ClassId cid) : DartTypedDataBase(cid), IHasPropertySetters<DartTypedDataView>
{
    public DartTypedDataView() : this(ClassId.kTypedDataViewCid) { }

    public DartTypedData? Data { get; set; }
    public DartInteger? OffsetInBytes { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartTypedDataView> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddValue(e => e.Length);
        setters.AddRef(e => e.Data);
        setters.AddRef(e => e.OffsetInBytes);
    }
}
