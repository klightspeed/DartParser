namespace DartParser;

public record struct SizeShiftBits(int SizeShift, uint SizeMask, int ClassIdShift, ulong ClassIdMask, int HashShift, uint HashMask);
