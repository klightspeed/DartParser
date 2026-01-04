using DartParser.Dart;
using DartParser.Dart.Objects;
using Semver;
using System.Runtime.Intrinsics;

namespace DartParser.Dart.Objects.ToCheck;

public class DartTypedData(ClassId cid) : DartObject(cid)
{
    protected static readonly Dictionary<Type, ClassId> ClassIds = new()
    {
        [typeof(byte)] = ClassId.kTypedDataUint8ArrayCid,
        [typeof(sbyte)] = ClassId.kTypedDataInt8ArrayCid,
        [typeof(ushort)] = ClassId.kTypedDataUint16ArrayCid,
        [typeof(short)] = ClassId.kTypedDataInt16ArrayCid,
        [typeof(uint)] = ClassId.kTypedDataUint32ArrayCid,
        [typeof(int)] = ClassId.kTypedDataInt32ArrayCid,
        [typeof(ulong)] = ClassId.kTypedDataUint64ArrayCid,
        [typeof(long)] = ClassId.kTypedDataInt64ArrayCid,
        [typeof(float)] = ClassId.kTypedDataFloat32ArrayCid,
        [typeof(double)] = ClassId.kTypedDataFloat64ArrayCid,
        [typeof(Vector128<int>)] = ClassId.kTypedDataInt32x4ArrayCid,
        [typeof(Vector128<float>)] = ClassId.kTypedDataFloat32x4ArrayCid,
        [typeof(Vector128<double>)] = ClassId.kTypedDataFloat64x2ArrayCid,
    };

    public DartTypedData() : this(ClassId.kTypedDataCid) { }
}
