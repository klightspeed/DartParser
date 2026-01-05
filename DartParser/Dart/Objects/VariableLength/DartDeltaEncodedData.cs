using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.VariableLength
{
    public class DartDeltaEncodedData() : DartTypedDataBase(), IHasPropertySetters<DartDeltaEncodedData>, IHasData<byte>
    {
        public ulong LengthInBytes { get; set; }
        public ulong LengthAndType { get; set; }
        public uint[] Data { get; set; } = [];

        ulong IHasData<byte>.Length => LengthInBytes;
        byte[] IHasData<byte>.Data { get; set; } = [];

        public static void InitPropertySetters(DartPropertySetters<DartDeltaEncodedData> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddUnsigned(e => e.LengthAndType);
        }
    }
}
