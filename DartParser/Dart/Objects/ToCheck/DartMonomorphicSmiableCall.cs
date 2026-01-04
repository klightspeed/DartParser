using DartParser.Dart;
using DartParser.Dart.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.ToCheck
{
    public class DartMonomorphicSmiableCall() : DartObject(ClassId.kMonomorphicSmiableCallCid)
    {
        public ulong ExpectedCid { get; set; }
        public ulong EntryPoint { get; set; }
    }
}
