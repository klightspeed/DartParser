using DartParser.Dart.Objects.Canonical;
using DartParser.Dart.Objects.FixedSize;
using DartParser.Dart.Objects.VariableLength;

namespace DartParser.Dart.Objects.BaseTypes;

public class DartLinkedHashBase(ClassId cid) : DartInstance(cid)
{
    public DartTypeArguments? TypeArguments { get; set; }
    public DartInteger? HashMask { get; set; }
    public DartArray? Data { get; set; }
    public DartInteger? UsedData { get; set; }
    public DartInteger? DeletedKeys { get; set; }
}
