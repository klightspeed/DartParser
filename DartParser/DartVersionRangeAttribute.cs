using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DartVersionRangeAttribute(string versionRange) : Attribute
    {
        public string VersionRange { get; set; } = versionRange;
    }
}
