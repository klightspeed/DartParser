using DartParser.Dart.Objects.BaseTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.Singletons
{
    public class DartNull : DartObject
    {
        private DartNull() : base("null", ClassId.kNullCid) { }

        public static DartNull Instance { get; } = new DartNull();
    }
}
