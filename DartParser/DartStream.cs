using DartParser.Dart;
using DartParser.Dart.Objects;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DartParser
{
    public class DartStream(Memory<byte> buffer)
    {
        const int kDataBitsPerByte = 7;
        const int kByteMask = (1 << kDataBitsPerByte) - 1;
        const int kMaxUnsignedDataPerByte = kByteMask;
        const int kMinDataPerByte = -(1 << (kDataBitsPerByte - 1));
        const int kMaxDataPerByte = (~kMinDataPerByte & kByteMask);
        const int kEndByteMarker = (byte.MaxValue - kMaxDataPerByte);
        const int kEndUnsignedByteMarker = (byte.MaxValue - kMaxUnsignedDataPerByte);


        private readonly Memory<byte> Buffer = buffer;
        private Memory<byte> Current = buffer;

        public static DartStream Empty { get; } = new DartStream(Memory<byte>.Empty);

        public void ReadBytes(Span<byte> buffer, int len)
        {
            ReadBytes(len).CopyTo(buffer);
        }

        public ReadOnlySpan<byte> ReadBytes(int len)
        {
            var retspan = Current.Span[..len];
            Current = Current[len..];
            return retspan;
        }

        public ReadOnlyMemory<byte> ReadMemory(int len)
        {
            var retmem = Current[..len];
            Current = Current[len..];
            return retmem;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public void Advance(int len)
        {
            Current = Current[len..];
        }

        public int Position
        {
            get => Buffer.Length - Current.Length;
            set => Advance(value - Position);
        }

        public int PendingBytes => Current.Length;

        public void Align(int alignment, int offset = 0)
        {
            if (alignment <= 0) throw new ArgumentException("alignment must be greater than 0", nameof(alignment));
            if (offset < 0) throw new ArgumentException("offset must be greater than or equal to 0", nameof(offset));
            if (offset >= alignment) throw new ArgumentException("offset must be less than alignment", nameof(offset));
            if ((alignment & (alignment - 1)) != 0) throw new ArgumentException("alignment must be a power of 2", nameof(offset));
            offset += Position & (1 - alignment);
            if (offset >= alignment) offset -= alignment;
            Advance(offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public byte ReadByte()
        {
            byte b = Current.Span[0];
            Current = Current[1..];
            return b;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public T ReadSLEB<T>()
            where T : unmanaged, IBinaryInteger<T>, ISignedNumber<T>
        {
            int shift = 0;
            byte b;
            T val = T.Zero;

            do
            {
                b = ReadByte();
                val |= T.CreateChecked(b & 0x7F) << shift;
                shift += 7;
            }
            while ((b & 0x80) != 0);

            T sign_bits = T.Zero;

            if ((b & 0x40) != 0)
            {
                sign_bits = T.NegativeOne << shift;
            }

            return val | sign_bits;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public T ReadLEB128<T>()
            where T : unmanaged, IBinaryInteger<T>, IUnsignedNumber<T>
        {
            int shift = 0;
            byte b;
            T val = T.Zero;

            do
            {
                b = ReadByte();
                val |= T.CreateChecked(b & 0x7F) << shift;
                shift += 7;
            }
            while ((b & 0x80) != 0);

            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private T Read<T>(byte endByteMarker)
            where T : unmanaged, IBinaryInteger<T>
        {
            if (Unsafe.SizeOf<T>() == 1)
            {
                if (T.AllBitsSet < T.Zero)
                {
                    return T.CreateChecked((sbyte)ReadByte());
                }
                else
                {
                    return T.CreateChecked(ReadByte());
                }
            }

            int shift = 0;
            byte b;
            T val = T.Zero;

            do
            {
                b = ReadByte();
                val |= T.CreateChecked(b) << shift;
                shift += 7;
            } while (b <= kMaxUnsignedDataPerByte);

            val -= T.CreateChecked(endByteMarker) << (shift - 7);

            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public T ReadCast<T, TInt>()
            where T : unmanaged
            where TInt : unmanaged, IBinaryInteger<TInt>
        {
            return Unsafe.BitCast<TInt, T>(Read<TInt>(kEndByteMarker));
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public T ReadValue<T>()
            where T : unmanaged
        {
            return Unsafe.SizeOf<T>() switch
            {
                1 => ReadCast<T, byte>(),
                2 => ReadCast<T, ushort>(),
                4 => ReadCast<T, uint>(),
                8 => ReadCast<T, ulong>(),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public ulong ReadUnsigned() => Read<ulong>(kEndUnsignedByteMarker);

        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public ClassId ReadCid(ClassTable classTable)
        {
            return classTable.LookupClassId(ReadValue<ulong>());
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public ulong ReadRefId()
        {
            byte b;
            ulong val = 0;

            do
            {
                b = ReadByte();
                val = (val << 7) | (b & 0x7Fu);
            } while ((b & 0x80) == 0);

            return val;
        }
    }
}
