using DartParser.Dart;
using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.Other;

namespace DartParser.Dart.Objects.ToCheck;

public class DartSingleTargetCache() : DartObject(ClassId.kSingleTargetCacheCid)
{
    public DartCode? Target { get; set; }
}
