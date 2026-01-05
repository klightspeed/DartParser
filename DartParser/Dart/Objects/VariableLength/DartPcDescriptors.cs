using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.VariableLength
{
    public class DartPcDescriptors() : DartObject(ClassId.kPcDescriptorsCid), IHasPropertySetters<DartPcDescriptors>, IHasData<byte>
    {
        public ulong Length { get; set; }
        public byte[] Data { get; set; } = [];


        public static void InitPropertySetters(DartPropertySetters<DartPcDescriptors> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddUnsigned(e => e.Length);
        }
    }
}
