using DartParser.Dart.Objects.BaseTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.Other
{
    public class DartUserDefinedInstance(ClassId cid) : DartInstance(cid)
    {
        public DartClass? Class { get; set; }

        public record struct Field
        {
            public bool IsRawValue { get; set; }
            public ulong RawValue { get; set; }
            public DartObject? RawObject { get; set; }
        }

        public Field[] Fields { get; set; } = [];
    }
}
