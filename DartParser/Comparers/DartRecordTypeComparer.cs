using DartParser.Dart.Objects.Canonical;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DartParser.Comparers
{
    public class DartRecordTypeComparer : IEqualityComparer<DartRecordType>
    {
        public static DartRecordTypeComparer Instance { get; } = new();

        public bool Equals(DartRecordType? x, DartRecordType? y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] DartRecordType obj)
        {
            throw new NotImplementedException();
        }
    }
}
