namespace DartParser.Dart;

public enum SnapshotKind : ulong
{
    kFull,      // Full snapshot of an application.
    kFullCore,  // Full snapshot of core libraries.
    kFullJIT,   // Full + JIT code
    kFullAOT,   // Full + AOT code
    kModule,    // Module snapshot with code.
    kNone,      // gen_snapshot
    kInvalid
}
