using DartParser.Dart;
using DartParser.Dart.Objects;
using Semver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.FixedSize
{
    public class DartLoadingUnit() : DartObject(ClassId.kLoadingUnitCid), IDartObject<DartLoadingUnit>
    {
        public DartLoadingUnit? Parent { get; set; }
        public ulong Id { get; set; }

        public static void InitPropertySetters(DartPropertySetters<DartLoadingUnit> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddRef(e => e.Parent);
            setters.AddValue(e => e.Id);
        }
    }
}
