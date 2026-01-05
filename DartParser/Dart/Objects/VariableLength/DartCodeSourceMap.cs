using DartParser.Dart.Objects.BaseTypes;
using Semver;

namespace DartParser.Dart.Objects.VariableLength;

public class DartCodeSourceMap() : DartObject(ClassId.kCodeSourceMapCid), IHasPropertySetters<DartCodeSourceMap>, IHasData<byte>
{
    public ulong Length { get; set; }
    public byte[] Data { get; set; } = [];

    public static void InitPropertySetters(DartPropertySetters<DartCodeSourceMap> setters, SemVersion version, SnapshotKind kind, bool isProduct)
    {
        setters.AddUnsigned(e => e.Length);
    }
}
