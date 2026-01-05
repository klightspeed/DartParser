using DartParser.Dart.Objects.BaseTypes;
using Semver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.VariableLength
{
    public class DartRecord() : DartInstance(ClassId.kRecordCid), IHasPropertySetters<DartRecord>, IHasData<DartObject>
    {
        public ulong Shape { get; set; }
        public DartObject?[] Data { get; set; } = [];

        ulong IHasData<DartObject>.Length => Shape & 0xFFFF;

        public static void InitPropertySetters(DartPropertySetters<DartRecord> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddUnsigned(e => e.Shape);
        }
    }
}
