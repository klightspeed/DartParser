using DartParser.Dart;
using DartParser.Dart.Objects;
using DartParser.Dart.Objects.FixedSize;

namespace DartParser.Dart.Objects.ToCheck;

public class DartBytecode() : DartObject(ClassId.kBytecodeCid)
{
    public ulong Instructions { get; set; }
    public ulong InstructionsSize { get; set; }

    [DartField]
    public DartObjectPool? ObjectPool { get; set; }

    [DartField]
    public DartFunction? Function { get; set; }

    [DartField]
    public DartArray? Closures { get; set; }

    [DartField]
    public DartTypedData? Binary { get; set; }

    [DartField]
    public DartExceptionHandlers? ExceptionHandlers { get; set; }

    [DartField(NotInAOT = true, NotInProduct = true)]
    public DartLocalVarDescriptors? VarDescriptors { get; set; }

    [DartField]
    [LastSnapshotFieldFor(All = true)]
    public DartPcDescriptors? PCDescriptors { get; set; }

    public ulong InstructionsBinaryOffset { get; set; }
    public ulong CodeOffset { get; set; }
    public ulong SourcePositionsBinaryOffset { get; set; }
    public ulong LocalVariablesBinaryOffset { get; set; }
}
