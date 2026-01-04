using DartParser.Dart;
using DartParser.Dart.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.ToCheck
{
    public class DartRecordType() : DartAbstractType(ClassId.kRecordTypeCid)
    {
        [DartField]
        public DartObject? Shape { get; set; }

        [DartField]
        [LastSnapshotFieldFor(All = true)]
        public DartArray? FieldTypes { get; set; }
    }
}
