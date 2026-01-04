using DartParser.Dart;
using DartParser.Dart.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.ToCheck
{
    public class DartWeakArray() : DartObject(ClassId.kWeakArrayCid)
    {
        public ulong Length { get; set; }
        public List<DartObject?> Data { get; set; } = [];
    }
}
