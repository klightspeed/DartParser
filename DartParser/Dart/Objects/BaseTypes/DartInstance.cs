namespace DartParser.Dart.Objects.BaseTypes;

public class DartInstance(ClassId cid) : DartObject(cid)
{
    public DartInstance() : this(ClassId.kInstanceCid) { }
}
