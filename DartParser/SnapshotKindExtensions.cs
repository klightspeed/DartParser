namespace DartParser;

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
