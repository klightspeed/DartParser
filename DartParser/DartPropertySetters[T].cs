using DartParser.Dart;
using DartParser.Dart.Objects.BaseTypes;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;

namespace DartParser;

public partial class DartPropertySetters<T> : DartPropertySetters
{
    public delegate void Setter(ref T obj, Snapshot snapshot);
    public delegate void PropSetter<TValue>(ref T obj, TValue val);
    public delegate TValue Fetcher<TValue>(Snapshot snapshot, ref T obj);
    public delegate bool Predicate(ref T obj);

    public record struct PropSetterInfo<TValue>(PropSetter<TValue> Setter, string PropertyName);
    public record class SetterInfo(string PropertyName, Type PropertyType, Type ValueType, Setter Setter);

    public List<SetterInfo> Setters { get; } = [];

    public override void FillFields<U>(U obj, Snapshot snapshot)
    {
        if (obj is not T tobj) throw new ArgumentException("Invalid type", nameof(obj));

        foreach (var setter in Setters)
        {
            setter.Setter(ref tobj, snapshot);
        }
    }

    public override void FillFields<U>(ref U obj, Snapshot snapshot)
    {
        if (obj is not T tobj) throw new ArgumentException("Invalid type", nameof(obj));

        for (int i = 0; i < Setters.Count; i++)
        {
            var setter = Setters[i];
            setter.Setter(ref tobj, snapshot);
        }
    }

    private static PropSetterInfo<TValue> MakePropSetter<TValue>(Expression<Func<T, TValue>> selector)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));
        var refParam = Expression.Parameter(typeof(T).MakeByRefType(), "e");
        var valParam = Expression.Parameter(typeof(TValue), "v");
        if (selector.Body is not MemberExpression memberExpr) throw new ArgumentException("Must be a property or field accessor", nameof(selector));

        switch (memberExpr)
        {
            case { Member: not PropertyInfo and not FieldInfo }:
                throw new ArgumentException("Must be a property or field accessor", nameof(selector));
            case { Member: PropertyInfo { CanWrite: false } }:
                throw new ArgumentException("Property must have a setter", nameof(selector));
            case { Member: FieldInfo { IsInitOnly: true } }:
                throw new ArgumentException("Field must not be readonly", nameof(selector));
        }

        return new(Expression.Lambda<PropSetter<TValue>>(
            Expression.Assign(
                new ParameterReplacerExpressionVisitor(selector.Parameters[0], refParam).Visit(selector.Body),
                valParam
            ),
            refParam,
            valParam
        ).Compile(), memberExpr.Member.Name);
    }

    private static void ExecuteSetter<TValue>(PropSetter<TValue> setter, Fetcher<TValue> fetch, ref T obj, Snapshot snapshot)
    {
        var val = fetch(snapshot, ref obj);
        setter(ref obj, val);
    }

    private static void ExecuteSetter<TValue, TFetch>(PropSetter<TValue> setter, Fetcher<TFetch> fetch, ref T obj, Snapshot snapshot)
        where TValue : unmanaged, IBinaryNumber<TValue>
        where TFetch : unmanaged, IBinaryNumber<TFetch>
    {
        var val = fetch(snapshot, ref obj);
        var cvtval = TValue.CreateChecked(val);
        setter(ref obj, cvtval);
    }

    private static void ExecuteSetterIf<TValue>(PropSetter<TValue> setter, Fetcher<TValue> fetch, Predicate predicate, ref T obj, Snapshot snapshot)
    {
        if (predicate(ref obj))
        {
            var val = fetch(snapshot, ref obj);
            setter(ref obj, val);
        }
    }

    private static void ExecuteSetterIf<TValue, TFetch>(PropSetter<TValue> setter, Fetcher<TFetch> fetch, Predicate predicate, ref T obj, Snapshot snapshot)
        where TValue : unmanaged, IBinaryNumber<TValue>
        where TFetch : unmanaged, IBinaryNumber<TFetch>
    {
        if (predicate(ref obj))
        {
            var val = fetch(snapshot, ref obj);
            var cvtval = TValue.CreateChecked(val);
            setter(ref obj, cvtval);
        }
    }

    private static SetterInfo MakeSetter<TValue>(Expression<Func<T, TValue>> selector, Fetcher<TValue> fetch)
    {
        var setter = MakePropSetter(selector);
        return new(setter.PropertyName, typeof(TValue), typeof(TValue), (ref e, s) => ExecuteSetter(setter.Setter, fetch, ref e, s));
    }

    private static SetterInfo MakeSetterConverted<TValue, TFetch>(Expression<Func<T, TValue>> selector, Fetcher<TFetch> fetch)
        where TValue : unmanaged, IBinaryNumber<TValue>
        where TFetch : unmanaged, IBinaryNumber<TFetch>
    {
        var setter = MakePropSetter(selector);
        return new(setter.PropertyName, typeof(TValue), typeof(TFetch), (ref e, s) => ExecuteSetter(setter.Setter, fetch, ref e, s));
    }

    private static SetterInfo MakeSetterIf<TValue>(Expression<Func<T, TValue>> selector, Fetcher<TValue> fetch, Predicate predicate)
    {
        var setter = MakePropSetter(selector);
        return new(setter.PropertyName, typeof(TValue), typeof(TValue), (ref e, s) => ExecuteSetterIf(setter.Setter, fetch, predicate, ref e, s));
    }

    private static SetterInfo MakeSetterConvertedIf<TValue, TFetch>(Expression<Func<T, TValue>> selector, Fetcher<TFetch> fetch, Predicate predicate)
        where TValue : unmanaged, IBinaryNumber<TValue>
        where TFetch : unmanaged, IBinaryNumber<TFetch>
    {
        var setter = MakePropSetter(selector);
        return new(setter.PropertyName, typeof(TValue), typeof(TFetch), (ref e, s) => ExecuteSetterIf(setter.Setter, fetch, predicate, ref e, s));
    }

    public void AddRef<TValue>(Expression<Func<T, TValue?>> selector)
        where TValue : DartObject
    {
        Setters.Add(MakeSetter(selector, (s, ref _) => s.ReadRef<TValue>()));
    }

    public void AddRefIf<TValue>(Expression<Func<T, TValue?>> selector, Predicate condition)
        where TValue : DartObject
    {
        Setters.Add(MakeSetterIf(selector, (s, ref _) => s.ReadRef<TValue>(), condition));
    }

    public void AddRefId(Expression<Func<T, DartRefId>> selector)
    {
        Setters.Add(MakeSetter(selector, (s, ref _) => s.ReadRefId()));
    }

    public void AddRefIdIf(Expression<Func<T, DartRefId>> selector, Predicate condition)
    {
        Setters.Add(MakeSetterIf(selector, (s, ref _) => s.ReadRefId(), condition));
    }

    public void AddValue<TValue>(Expression<Func<T, TValue>> selector)
        where TValue : unmanaged
    {
        Setters.Add(MakeSetter(selector, (s, ref _) => s.ReadValue<TValue>()));
    }

    public void AddValueIf<TValue>(Expression<Func<T, TValue>> selector, Predicate condition)
        where TValue : unmanaged
    {
        Setters.Add(MakeSetterIf(selector, (s, ref _) => s.ReadValue<TValue>(), condition));
    }

    public void AddValue<TValue, TFetch>(Expression<Func<T, TValue>> selector, Fetcher<TFetch> fetch)
        where TValue : unmanaged, IBinaryNumber<TValue>
        where TFetch : unmanaged, IBinaryNumber<TFetch>
    {
        Setters.Add(MakeSetterConverted(selector, fetch));
    }

    public void AddValueIf<TValue, TFetch>(Expression<Func<T, TValue>> selector, Fetcher<TFetch> fetch, Predicate condition)
        where TValue : unmanaged, IBinaryNumber<TValue>
        where TFetch : unmanaged, IBinaryNumber<TFetch>
    {
        Setters.Add(MakeSetterConvertedIf(selector, fetch, condition));
    }

    public void AddRaw<TValue>(Expression<Func<T, TValue>> selector)
        where TValue : struct
    {
        Setters.Add(MakeSetter(selector, (s, ref _) => s.ReadRaw<TValue>()));
    }

    public void AddRawIf<TValue>(Expression<Func<T, TValue>> selector, Predicate condition)
        where TValue : struct
    {
        Setters.Add(MakeSetterIf(selector, (s, ref _) => s.ReadRaw<TValue>(), condition));
    }

    public void AddTypedData<TValue>(Expression<Func<T, TValue[]>> selector)
        where TValue : struct
    {
        Setters.Add(MakeSetter(selector, (s, ref _) => s.ReadTypedData<TValue>()));
    }

    public void AddTypedDataIf<TValue>(Expression<Func<T, TValue[]>> selector, Predicate condition)
        where TValue : struct
    {
        Setters.Add(MakeSetterIf(selector, (s, ref _) => s.ReadTypedData<TValue>(), condition));
    }

    public void AddUnsigned<TValue>(Expression<Func<T, TValue>> selector)
        where TValue : unmanaged, IBinaryInteger<TValue>
    {
        Setters.Add(MakeSetterConverted(selector, (s, ref _) => s.ReadUnsigned()));
    }

    public void AddUnsignedIf<TValue>(Expression<Func<T, TValue>> selector, Predicate condition)
        where TValue : unmanaged, IBinaryInteger<TValue>
    {
        Setters.Add(MakeSetterConvertedIf(selector, (s, ref _) => s.ReadUnsigned(), condition));
    }

    public void AddCid(Expression<Func<T, ClassId>> selector)
    {
        Setters.Add(MakeSetter(selector, (s, ref _) => s.ReadCid()));
    }
}
