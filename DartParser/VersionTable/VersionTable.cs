using DartParser.Dart;
using Semver;

namespace DartParser;

public static partial class VersionTable
{
    public static SemVersion GetVersionFromSnapshotHash(string hash)
    {
        if (InconsistentClassIdHashes.Contains(hash))
        {
            throw new NotSupportedException($"Class ids cannot be determined for snapshot hash {hash}");
        }

        if (!HashVersions.TryGetValue(hash, out var version))
        {
            throw new NotSupportedException($"Unknown hash {hash}");
        }

        return version;
    }

    public static readonly Dictionary<SemVersion, List<ObjectTagFlags>> ObjectFlagBits = new()
    {
        [SemVersion.Parse("3.5.0-211.0.dev-edge-8")] = [
            ObjectTagFlags.CardRemembered,
            ObjectTagFlags.Canonical,
            ObjectTagFlags.NotMarked,
            ObjectTagFlags.NewOrEvacuationCandidate,
            ObjectTagFlags.AlwaysSet,
            ObjectTagFlags.OldAndNotRemembered,
            ObjectTagFlags.Immutable
        ],
        [SemVersion.Parse("3.4.0-79.0.dev-edge-4")] = [
            ObjectTagFlags.CardRemembered,
            ObjectTagFlags.Canonical,
            ObjectTagFlags.NotMarked,
            ObjectTagFlags.New,
            ObjectTagFlags.AlwaysSet,
            ObjectTagFlags.OldAndNotRemembered,
            ObjectTagFlags.Immutable
        ],
        [SemVersion.Parse("2.18.0-224.0.dev-edge-4")] = [
            ObjectTagFlags.CardRemembered,
            ObjectTagFlags.Canonical,
            ObjectTagFlags.OldAndNotMarked,
            ObjectTagFlags.New,
            ObjectTagFlags.Old,
            ObjectTagFlags.OldAndNotRemembered,
            ObjectTagFlags.Immutable
        ],
        [SemVersion.Parse("2.1.0-dev.8.0-edge-216")] = [
            ObjectTagFlags.CardRemembered,
            ObjectTagFlags.OldAndNotMarked,
            ObjectTagFlags.New,
            ObjectTagFlags.Old,
            ObjectTagFlags.OldAndNotRemembered,
            ObjectTagFlags.Canonical
        ],
        [SemVersion.Parse("2.1.0-dev.3.0-edge-58")] = [
            0,
            ObjectTagFlags.OldAndNotMarked,
            ObjectTagFlags.New,
            ObjectTagFlags.Old,
            ObjectTagFlags.OldAndNotRemembered,
            ObjectTagFlags.Canonical,
            ObjectTagFlags.VMHeapObject,
            ObjectTagFlags.GraphMarked
        ],
        [SemVersion.Parse("1.17.0-dev.4.0-edge-76")] = [
            ObjectTagFlags.Mark,
            ObjectTagFlags.Canonical,
            ObjectTagFlags.VMHeapObject,
            ObjectTagFlags.Remembered
        ],
        [SemVersion.Parse("0.5.0-1-edge-226")] = [
            ObjectTagFlags.Watched,
            ObjectTagFlags.Mark,
            ObjectTagFlags.Canonical,
            ObjectTagFlags.VMHeapObject,
            ObjectTagFlags.Remembered
        ],
    };

    public static readonly Dictionary<SemVersion, (SizeShiftBits Bits32, SizeShiftBits Bits64)> ObjectTagSizeShift = new()
    {
        [SemVersion.Parse("2.19.0-406.0.dev-edge-44")] = (
            new(8, 0x0F, 12, 0xFFFFF, 32, 0xFFFFFFFF),
            new(8, 0x0F, 12, 0xFFFFF, 32, 0xFFFFFFFF)
        ),
        [SemVersion.Parse("1.24.0-dev.5.0-edge-40")] = (
            new(8, 0xFF, 16, 0xFFFF, 32, 0xFFFFFFFF),
            new(8, 0xFF, 16, 0xFFFF, 32, 0xFFFFFFFF)
        ),
        [SemVersion.Parse("1.11.0-dev.5.0-edge-142")] = (
            new(8, 0xFF, 16, 0xFFFF, 0, 0),
            new(16, 0xFFFF, 32, 0xFFFFFFFF, 0, 0)
        ),
        [SemVersion.Parse("0.0.0")] = (
            new(8, 0xFF, 16, 0xFFFF, 0, 0),
            new(8, 0xFF, 16, 0xFFFF, 0, 0)
        )
    };
}
