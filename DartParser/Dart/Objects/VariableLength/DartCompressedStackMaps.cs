using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.VariableLength;

public class DartCompressedStackMaps() : DartObject(ClassId.kCompressedStackMapsCid), IHasPropertySetters<DartCompressedStackMaps>, IHasData<byte>
{
    public ulong FlagsAndSize { get; set; }
    public byte[] Data { get; set; } = [];

    ulong IHasData<byte>.Length => FlagsAndSize >> 2;

    public static void InitPropertySetters(DartPropertySetters<DartCompressedStackMaps> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddUnsigned(e => e.FlagsAndSize);
    }
}
