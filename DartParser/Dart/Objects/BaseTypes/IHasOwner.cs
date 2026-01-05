using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.BaseTypes
{
    public interface IHasOwner
    {
        DartObject? Owner { get; }
    }
}
