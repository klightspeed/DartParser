using DartParser.Dart;
using DartParser.Dart.Objects;
using Semver;

namespace DartParser.Dart.Objects.ToCheck;

public class DartTypedDataBase<T>(ClassId cid) : DartTypedDataBase(cid), IDartObject<DartTypedDataBase<T>>
    where T : struct
{
    public DartTypedDataBase() : this(ClassIds[typeof(T)]) { }

    public T[] Data { get; set; } = [];

    public static void InitPropertySetters(DartPropertySetters<DartTypedDataBase<T>> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddTypedData(e => e.Data);
    }
}
