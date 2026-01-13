using DartParser.Dart.Objects.BaseTypes;

namespace DartParser.Dart.Objects.Other;

public class DartUserDefinedInstance(ClassId cid) : DartInstance(cid)
{
    public record struct Field
    {
        public bool IsRawValue { get; set; }
        public ulong RawValue { get; set; }
        public DartObject? RawObject { get; set; }
    }

    public Field[] Fields { get; set; } = [];
}
