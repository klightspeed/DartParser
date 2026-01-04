using DartParser;
using System.Diagnostics;

DartIsolateGroup.TryProcessSnapshotFile(args[0], out var isolateGroup);

Debugger.Break();