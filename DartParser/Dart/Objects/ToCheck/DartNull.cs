using DartParser.Dart;
using DartParser.Dart.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.ToCheck
{
    public class DartNull : DartObject
    {
        private DartNull() : base("null", ClassId.kNullCid) { }

        public static DartNull Instance { get; } = new DartNull();
    }
}
