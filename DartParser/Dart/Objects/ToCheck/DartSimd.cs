using DartParser.Dart;
using DartParser.Dart.Objects;
using Semver;
using System.Runtime.Intrinsics;

namespace DartParser.Dart.Objects.ToCheck;

public class DartSimd<T>(ClassId cid) : DartObject(cid), IDartObject<DartSimd<T>>
    where T : unmanaged
{
    public DartSimd() : this(System.Type.GetTypeCode(typeof(T)) switch
    {
        TypeCode.Int32 => ClassId.kInt32x4Cid,
        TypeCode.Single => ClassId.kFloat32x4Cid,
        TypeCode.Double => ClassId.kFloat64x2Cid,
        _ => throw new NotSupportedException()
    })
    {
    }

    public Vector128<T> Value { get; set; }

    public static void InitPropertySetters(DartPropertySetters<DartSimd<T>> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddRaw(e => e.Value);
    }
}
