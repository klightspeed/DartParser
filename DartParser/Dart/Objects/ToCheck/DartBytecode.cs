using DartParser.Dart;
using DartParser.Dart.Objects.BaseTypes;
using DartParser.Dart.Objects.FixedSize;
using DartParser.Dart.Objects.VariableLength;

namespace DartParser.Dart.Objects.ToCheck;

public class DartBytecode() : DartObject(ClassId.kBytecodeCid)
{
    public ulong Instructions { get; set; }
    public ulong InstructionsSize { get; set; }
    public DartObjectPool? ObjectPool { get; set; }
    public DartFunction? Function { get; set; }
    public DartArray? Closures { get; set; }
    public DartTypedData? Binary { get; set; }
    public DartExceptionHandlers? ExceptionHandlers { get; set; }
    public DartLocalVarDescriptors? VarDescriptors { get; set; }
    public DartPcDescriptors? PCDescriptors { get; set; }
    public ulong InstructionsBinaryOffset { get; set; }
    public ulong CodeOffset { get; set; }
    public ulong SourcePositionsBinaryOffset { get; set; }
    public ulong LocalVariablesBinaryOffset { get; set; }
}
