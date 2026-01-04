using Semver;

namespace DartParser.Dart.Objects.ToCheck;

public class DartTypedDataView(ClassId cid) : DartTypedDataBase(cid), IDartObject<DartTypedDataView>
{
    public DartTypedDataView() : this(ClassId.kTypedDataViewCid) { }

    public DartTypedData? Data { get; set; }
    public DartInteger? OffsetInBytes { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartTypedDataView> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRef(e => e.Length);
        setters.AddRef(e => e.Data);
        setters.AddRef(e => e.OffsetInBytes);
    }
}
