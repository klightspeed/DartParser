using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.BaseTypes
{
    public interface IHasData<T>
    {
        ulong Length { get; }
        T?[] Data { get; set; }
    }
}
