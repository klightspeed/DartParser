using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser
{
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

    public static class SnapshotKindExtensions
    {
        extension(SnapshotKind kind)
        {
            public bool IsFull()
                => kind == SnapshotKind.kFull
                || kind == SnapshotKind.kFullCore
                || kind == SnapshotKind.kFullJIT
                || kind == SnapshotKind.kFullAOT;

            public bool IncludesCode()
                => kind == SnapshotKind.kFullJIT
                || kind == SnapshotKind.kFullAOT
                || kind == SnapshotKind.kModule;
        }
    }
}
