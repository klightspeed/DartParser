using DartParser.Dart;
using DartParser.Dart.Objects;
using Semver;

namespace DartParser.Dart.Objects.ToCheck
{
    public class DartPcDescriptors() : DartObject(ClassId.kPcDescriptorsCid), IDartObject<DartPcDescriptors>
    {
        public ulong Length { get; set; }
        public byte[] Data { get; set; } = [];

        public static void InitPropertySetters(DartPropertySetters<DartPcDescriptors> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddTypedData(e => e.Data);
        }
    }
}
