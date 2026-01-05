namespace DartParser.Dart.Objects.BaseTypes;

public class DartTypedDataBase<T>(ClassId cid) : DartTypedDataBase(cid)
    where T : struct;
