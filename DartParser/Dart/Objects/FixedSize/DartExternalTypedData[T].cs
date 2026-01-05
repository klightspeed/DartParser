using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.FixedSize;

public class DartExternalTypedData<T>(ClassId cid) : DartTypedDataBase<T>(cid), IHasPropertySetters<DartExternalTypedData<T>>, IHasData<T>
    where T : struct
{
    public DartExternalTypedData() : this(ClassIds[typeof(T)] | (ClassId)TypedDataCidRemainder.External) { }

    public T[] Data { get; set; } = [];

    public static void InitPropertySetters(DartPropertySetters<DartExternalTypedData<T>> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddUnsigned(e => e.Length);
    }
}
