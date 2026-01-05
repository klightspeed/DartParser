using DartParser.Dart.Objects.BaseTypes;

namespace DartParser.Dart.Objects.ToCheck;

public class DartMonomorphicSmiableCall() : DartObject(ClassId.kMonomorphicSmiableCallCid)
{
    public ulong ExpectedCid { get; set; }
    public ulong EntryPoint { get; set; }
}
