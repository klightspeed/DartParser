using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.VariableLength;
using System.Diagnostics;
using System.Numerics;

namespace DartParser.Dart.Clusters.Other;

public class DeltaEncodedTypedDataDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterBase(isCanonical, isImmutable, cid, isRootUnit, version)
{
    private DartDeltaEncodedData[] Entries = [];

    public override void ReadAllocate(Snapshot snapshot)
        => Entries = ReadAllocateVariableLength<DartDeltaEncodedData, byte>(snapshot, ClassId);

    private static T AllBitsSet<T>()
        where T : IBinaryNumber<T>
        => T.AllBitsSet;

    public override void ReadFill(Snapshot snapshot)
    {
        if (Entries.Length == 0)
            return;

        foreach (var entry in Entries)
        {
            entry.LengthAndType = snapshot.ReadUnsigned();
            var length = entry.LengthAndType >> 1;
            var entrySize = (length & 1) == 0 ? 2U : 4U;
            var type = (length & 1) == 0 ? ClassId.kTypedDataUint16ArrayCid : ClassId.kTypedDataUint32ArrayCid;
            uint mask = (length & 1) == 0 ? AllBitsSet<ushort>() : AllBitsSet<uint>();
            Debug.Assert(length * entrySize <= entry.LengthInBytes);
            entry.Type = type;
            entry.Data = new uint[length];
            uint value = 0;

            for (int i = 0; i < (int)length; i++)
            {
                value = (value + (uint)snapshot.ReadUnsigned()) & mask;
                entry.Data[i] = value;
            }
        }
    }
}
