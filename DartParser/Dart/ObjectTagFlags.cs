namespace DartParser.Dart;

[Flags]
public enum ObjectTagFlags : uint
{
    CardRemembered = 0x0001,
    Canonical = 0x0002,
    NotMarked = 0x0004,
    NewOrEvacuationCandidate = 0x0008,
    AlwaysSet = 0x0010,
    OldAndNotRemembered = 0x0020,
    Immutable = 0x0040,
    Old = 0x0100,
    New = 0x0200,
    OldAndNotMarked = 0x0400,
    VMHeapObject = 0x0800,
    GraphMarked = 0x1000,
    Mark = 0x2000,
    Remembered = 0x4000,
    Watched = 0x8000,
}
