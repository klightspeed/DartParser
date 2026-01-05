using DartParser.Dart.Objects.BaseTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Clusters.BaseTypes
{
    public class CanonicalSetDeserializationCluster<T>(bool isCanonical, bool isImmutable, ClassId cid, bool isRootUnit, Semver.SemVersion version)
        : DeserializationClusterBase(isCanonical, isImmutable, cid, isRootUnit, version)
        where T : DartObject
    {
        protected T[] Entries { get; set; } = [];
        protected T?[] Table { get; set; } = [];

        protected void BuildCanonicalSetFromLayout(Snapshot snapshot)
        {
            if (!IsRootUnit || !IsCanonical)
            {
                return;
            }

            ulong table_length = snapshot.ReadUnsigned();
            ulong first_element = snapshot.ReadUnsigned();
            Table = new T[table_length];

            for (ulong i = first_element, pos = (ulong)Entries.Length - first_element; i < (ulong)Entries.Length; i++)
            {
                pos += snapshot.ReadUnsigned();
                Table[pos] = Entries[i];
            }
        }
    }
}
