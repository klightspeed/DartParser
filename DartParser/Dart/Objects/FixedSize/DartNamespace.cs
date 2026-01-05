using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.VariableLength;
using Semver;

namespace DartParser.Dart.Objects.FixedSize
{
    public class DartNamespace() : DartObject(ClassId.kNamespaceCid), IHasPropertySetters<DartNamespace>
    {
        public DartLibrary? Target { get; set; }
        public DartArray? ShowNames { get; set; }
        public DartArray? HideNames { get; set; }
        public DartLibrary? Owner { get; set; }

        public static void InitPropertySetters(DartPropertySetters<DartNamespace> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddRef(e => e.Target);

            if (kind != SnapshotKind.kFullAOT)
            {
                setters.AddRef(e => e.ShowNames);
                setters.AddRef(e => e.HideNames);
                setters.AddRef(e => e.Owner);
            }
        }
    }
}
