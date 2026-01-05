using DartParser.Dart.Objects.BaseTypes;
using Semver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartParser.Dart.Objects.VariableLength
{
    public class DartObjectPool() : DartObject(ClassId.kObjectPoolCid), IHasPropertySetters<DartObjectPool>, IHasData<DartObjectPool.Entry>
    {
        public ulong Length { get; set; }

        public enum SnapshotBehaviour
        {
            kSnapshotable,
            kNotSnapshotable,
            kResetToBootstrapNative,
            kResetToSwitchableCallMissEntryPoint,
            kSetToZero
        }

        public enum EntryType
        {
            kImmediate,
            kTaggedObject,
            kNativeFunction,
            kImmediateLarge
        }

        public record struct Entry : IHasPropertySetters<Entry>
        {
            public byte EntryBits { get; set; }
            public DartObject? RawObject { get; set; }
            public ulong RawValue { get; set; }

            public readonly EntryType EntryType => (EntryType)(EntryBits & 0x0F);
            public readonly bool Patchable => (EntryBits & 0x10) == 0;
            public readonly SnapshotBehaviour SnapshotBehaviour => (SnapshotBehaviour)((EntryBits >> 5) & 0x07);

            public static void InitPropertySetters(DartPropertySetters<Entry> setters, SemVersion version, SnapshotKind kind, bool isProduct)
            {
                setters.AddValue(e => e.EntryBits);
                setters.AddValueIf(e => e.RawValue, (ref e) => e.SnapshotBehaviour == SnapshotBehaviour.kSnapshotable && e.EntryType == EntryType.kImmediate);
                setters.AddRefIf(e => e.RawObject, (ref e) => e.SnapshotBehaviour == SnapshotBehaviour.kSnapshotable && e.EntryType == EntryType.kTaggedObject);
            }
        }

        public Entry[] Data { get; set; } = [];

        public static void InitPropertySetters(DartPropertySetters<DartObjectPool> setters, SemVersion version, SnapshotKind kind, bool isProduct)
        {
            setters.AddUnsigned(e => e.Length);
        }
    }
}
