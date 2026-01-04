using DartParser.Dart;
using DartParser.Dart.Objects;
using DartParser.Dart.Objects.BaseTypes;

namespace DartParser.Dart.Objects.ToCheck;

public class DartAbstractType(ClassId cid) : DartInstance(cid)
{
    public DartAbstractType() : this(ClassId.kAbstractTypeCid) { }

    public DartObject? TypeTestStub { get; set; }
    public DartObject? Hash { get; set; }

    public byte Flags { get; set; }
}
