using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.VariableLength;
using Semver;

namespace DartParser.Dart.Objects.FixedSize
{
    public class DartMegamorphicCache() : DartCallSiteData(ClassId.kMegamorphicCacheCid), IHasPropertySetters<DartMegamorphicCache>
    {
        public DartArray? Buckets { get; set; }
        public DartInteger? Mask { get; set; }
        public ulong FilledEntryCount { get; set; }

        public static void InitPropertySetters(DartPropertySetters<DartMegamorphicCache> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddRef(e => e.TargetName);
            setters.AddRef(e => e.ArgsDescriptor);
            setters.AddRef(e => e.Buckets);
            setters.AddRef(e => e.Mask);
            setters.AddValue(e => e.FilledEntryCount);
        }
    }
}
