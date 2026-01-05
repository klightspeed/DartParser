using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.FixedSize
{
    public class DartMap() : DartLinkedHashBase(ClassId.kMapCid), IHasPropertySetters<DartMap>
    {
        public static void InitPropertySetters(DartPropertySetters<DartMap> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddRef(e => e.TypeArguments);
            setters.AddRef(e => e.HashMask);
            setters.AddRef(e => e.Data);
            setters.AddRef(e => e.UsedData);
            setters.AddRef(e => e.DeletedKeys);
        }
    }
}
