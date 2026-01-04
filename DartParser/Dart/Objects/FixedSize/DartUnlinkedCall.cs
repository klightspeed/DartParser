using DartParser.Dart;
using DartParser.Dart.Objects;
using DartParser.Dart.Objects.ToCheck;
using Semver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.FixedSize
{
    public class DartUnlinkedCall() : DartCallSiteData(ClassId.kUnlinkedCallCid), IDartObject<DartUnlinkedCall>
    {
        public bool CanPatchToMonomorphic { get; set; }

        public static void InitPropertySetters(DartPropertySetters<DartUnlinkedCall> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddRef(e => e.TargetName);
            setters.AddRef(e => e.ArgsDescriptor);
            setters.AddValue(e => e.CanPatchToMonomorphic);
        }
    }
}
