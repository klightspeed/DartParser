using DartParser;
using DartParser.Dart;
using DartParser.Dart.Clusters.BaseTypes;
using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Other;
using System.Diagnostics;

namespace DartParser.Dart.Clusters.Other;

public class InstanceDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterBase(isCanonical, isImmutable, cid, isRootUnit, version)
{
    private DartUserDefinedInstance[] Entries = [];
    private ulong InstanceSizeInWords;
    private ulong NextFieldOffsetInWords;

    public override void ReadAllocate(Snapshot snapshot)
    {
        var count = snapshot.ReadUnsigned();
        NextFieldOffsetInWords = snapshot.Read<ulong>();
        InstanceSizeInWords = snapshot.Read<ulong>();
        Debug.Assert(NextFieldOffsetInWords <= InstanceSizeInWords);
        Debug.Assert(NextFieldOffsetInWords < 64);
        Entries = new DartUserDefinedInstance[count];

        for (var i = 0UL; i < count; i++)
        {
            var entry = new DartUserDefinedInstance(ClassId)
            {
                Fields = new DartUserDefinedInstance.Field[InstanceSizeInWords]
            };
            Entries[i] = entry;
            snapshot.AddObject(entry);
        }
    }

    public override void ReadFill(Snapshot snapshot)
    {
        var unboxedFields = snapshot.ReadUnsigned();

        foreach (var entry in Entries)
        {
            for (int i = 2; i < (int)NextFieldOffsetInWords; i++)
            {
                var isUnboxedField = ((unboxedFields >> i) & 1) != 0;

                if (isUnboxedField)
                {
                    entry.Fields[i].IsRawValue = true;
                    entry.Fields[i].RawValue = snapshot.Read<uint>();

                    if (snapshot.Is64Bit)
                    {
                        entry.Fields[i].RawValue |= (ulong)snapshot.Read<uint>() << 32;
                    }
                }
                else
                {
                    entry.Fields[i].IsRawValue = false;
                    entry.Fields[i].RawObject = snapshot.ReadRef<DartObject>();
                }
            }
        }
    }
}
