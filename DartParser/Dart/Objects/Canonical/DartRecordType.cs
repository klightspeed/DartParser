using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.VariableLength;
using Semver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.Canonical
{
    public class DartRecordType() : DartAbstractType(ClassId.kRecordTypeCid), IHasPropertySetters<DartRecordType>
    {
        public DartObject? Shape { get; set; }
        public DartArray? FieldTypes { get; set; }

        public static void InitPropertySetters(DartPropertySetters<DartRecordType> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddRef(e => e.TypeTestStub);
            setters.AddRef(e => e.Hash);
            setters.AddRef(e => e.Shape);
            setters.AddRef(e => e.FieldTypes);
            setters.AddValue(e => e.Flags, (s, ref _) => s.Read<byte>());
        }
    }
}
