namespace DartParser
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class LastSnapshotFieldForAttribute : Attribute
    {
        public bool All { get; set; }
        public SnapshotKind Kind { get; set; }
        public bool IsProduct { get; set; }

        public LastSnapshotFieldForAttribute()
        {
        }

        public LastSnapshotFieldForAttribute(SnapshotKind kind)
        {
            Kind = kind;
        }
    }
}
