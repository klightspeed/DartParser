using DartParser.Dart;
using DartParser.Dart.Clusters;
using DartParser.Dart.Objects.ToCheck;
using System.Diagnostics;

namespace DartParser.Dart.Clusters.ToCheck;

public class ClassDeserializationCluster(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
    : DeserializationClusterBase(isCanonical, isImmutable, cid, isRootUnit, version)
{
    private DartClass[] PredefinedClasses = [];
    private DartClass[] OtherClasses = [];

    public override void ReadAllocate(Snapshot snapshot)
    {
        var count = snapshot.ReadUnsigned();
        var classTable = snapshot.ClassTable;
        this.PredefinedClasses = new DartClass[count];

        for (var i = 0UL; i < count; i++)
        {
            var cid = snapshot.ReadCid();
            var cls = classTable[cid];
            Debug.Assert(cls != null);
            snapshot.Objects.Add(cls);
            PredefinedClasses[i] = cls;
        }

        count = snapshot.ReadUnsigned();
        this.OtherClasses = new DartClass[count];

        for (var i = 0UL; i < count; i++)
        {
            var cls = new DartClass();
            snapshot.Objects.Add(cls);
            OtherClasses[i] = cls;
        }
    }

    public override void ReadFill(Snapshot snapshot)
    {
        foreach (var cls in PredefinedClasses)
        {
            snapshot.FillFields(cls);
        }

        foreach (var cls in OtherClasses)
        {
            snapshot.FillFields(cls);
            snapshot.ClassTable[cls.ClassId] = cls;
        }
    }
}
