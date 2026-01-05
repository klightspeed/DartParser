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
}
