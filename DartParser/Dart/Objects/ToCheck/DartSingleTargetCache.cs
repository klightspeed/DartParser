using DartParser.Dart;
using DartParser.Dart.Objects;

namespace DartParser.Dart.Objects.ToCheck;

public class DartSingleTargetCache() : DartObject(ClassId.kSingleTargetCacheCid)
{
    [DartField]
    public DartCode? Target { get; set; }
}
