using DartParser.Dart;
using DartParser.Dart.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.ToCheck
{
    public class DartObjectPool() : DartObject(ClassId.kObjectPoolCid)
    {
        public ulong Length { get; set; }

        public struct Entry
        {
            public DartObject? RawObject { get; set; }
            public ulong RawValue { get; set; }
        }

        public List<Entry> Data { get; set; } = [];
    }
}
