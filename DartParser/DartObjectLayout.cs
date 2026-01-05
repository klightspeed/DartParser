namespace DartParser;

public abstract class DartObjectLayout
{
    public abstract void FillFields<T>(T obj, Snapshot snapshot, DartStream stream) where T : class;

    public abstract void FillFields<T>(ref T obj, Snapshot snapshot, DartStream stream);
}
