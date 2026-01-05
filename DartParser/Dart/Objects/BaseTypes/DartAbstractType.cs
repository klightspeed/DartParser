namespace DartParser.Dart.Objects.BaseTypes;

public class DartAbstractType(ClassId cid) : DartInstance(cid)
{
    public DartAbstractType() : this(ClassId.kAbstractTypeCid) { }

    public DartObject? TypeTestStub { get; set; }
    public DartObject? Hash { get; set; }

    public byte Flags { get; set; }
}
