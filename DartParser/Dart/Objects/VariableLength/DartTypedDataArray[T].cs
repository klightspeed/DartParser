using Semver;
using DartParser.Dart.Objects.BaseTypes;
using DotNext;

namespace DartParser.Dart.Objects.VariableLength;

public class DartTypedDataArray<T>(ClassId cid) : DartTypedDataBase<T>(cid), IHasPropertySetters<DartTypedDataArray<T>>, IHasData<T>
    where T : struct
{
    public DartTypedDataArray() : this(ClassIds[typeof(T)]) { }

    public T[] Data { get; set; } = [];

    public static void InitPropertySetters(DartPropertySetters<DartTypedDataArray<T>> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddUnsigned(e => e.Length);
    }
}
