using DartParser.Dart;
using DartParser.Dart.Objects;
using DartParser.Dart.Objects.FixedSize;
using Semver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.ToCheck
{
    public class DartLibraryPrefix() : DartObject(ClassId.kLibraryPrefixCid), IDartObject<DartLibraryPrefix>
    {
        public DartString? Name { get; set; }
        public DartArray? Imports { get; set; }
        public DartLibrary? Importer { get; set; }
        public ulong NumImports { get; set; }
        public ulong IsDeferredLoad { get; set; }

        public static void InitPropertySetters(DartPropertySetters<DartLibraryPrefix> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddRef(e => e.Name);
            setters.AddRef(e => e.Imports);

            if (kind != SnapshotKind.kFullAOT)
            {
                setters.AddRef(e => e.Importer);
            }

            setters.AddValue(e => e.NumImports);
            setters.AddValue(e => e.IsDeferredLoad);
        }
    }
}
