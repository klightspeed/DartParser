using DartParser.Dart;
using DartParser.Dart.Objects.BaseTypes;
using System.Linq.Expressions;
using System.Numerics;

namespace DartParser;

public partial class DartPropertySetters<T> : DartPropertySetters
{
    public delegate void Setter(ref T obj, Snapshot snapshot);

    public List<Setter> Setters { get; } = [];

    public override void FillFields<U>(U obj, Snapshot snapshot)
    {
        if (obj is not T tobj) throw new ArgumentException("Invalid type", nameof(obj));

        foreach (var setter in Setters)
        {
            setter(ref tobj, snapshot);
        }
    }

    public override void FillFields<U>(ref U obj, Snapshot snapshot)
    {
        if (obj is not T tobj) throw new ArgumentException("Invalid type", nameof(obj));

        foreach (var setter in Setters)
        {
            setter(ref tobj, snapshot);
        }
    }

    private static Setter MakeSetter<TValue>(Expression<Func<T, TValue>> selector, Expression<Func<Snapshot, T, TValue>> fetch)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));
        ArgumentNullException.ThrowIfNull(fetch, nameof(fetch));
        var refParam = Expression.Parameter(typeof(T).MakeByRefType(), "e");

        if (selector.Body is not MemberExpression) throw new ArgumentException("Invalid selector", nameof(selector));

        return Expression.Lambda<Setter>(
            Expression.Assign(
                new ParameterReplacerExpressionVisitor(selector.Parameters[0], refParam).Visit(selector.Body),
                new ParameterReplacerExpressionVisitor(fetch.Parameters[1], refParam).Visit(fetch.Body)
            ),
            refParam,
            fetch.Parameters[0]
        ).Compile();
    }

    private static Setter MakeSetterConverted<TValue, TFetch>(Expression<Func<T, TValue>> selector, Expression<Func<Snapshot, T, TFetch>> fetch)
        where TValue : unmanaged, IBinaryInteger<TValue>
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));
        ArgumentNullException.ThrowIfNull(fetch, nameof(fetch));
        var refParam = Expression.Parameter(typeof(T).MakeByRefType(), "e");

        if (selector.Body is not MemberExpression) throw new ArgumentException("Invalid selector", nameof(selector));

        return Expression.Lambda<Setter>(
            Expression.Assign(
                new ParameterReplacerExpressionVisitor(selector.Parameters[0], refParam).Visit(selector.Body),
                Expression.ConvertChecked(new ParameterReplacerExpressionVisitor(fetch.Parameters[1], refParam).Visit(fetch.Body), typeof(TValue))
            ),
            refParam,
            fetch.Parameters[0]
        ).Compile();
    }

    private static Setter MakeSetterIf<TValue>(Expression<Func<T, TValue>> selector, Expression<Func<Snapshot, T, TValue>> fetch, Expression<Func<T, bool>> condition)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));
        ArgumentNullException.ThrowIfNull(fetch, nameof(fetch));
        var refParam = Expression.Parameter(typeof(T).MakeByRefType(), "e");

        if (selector.Body is not MemberExpression) throw new ArgumentException("Invalid selector", nameof(selector));

        var visitor = new ParameterReplacerExpressionVisitor(condition.Parameters[0], selector.Parameters[0]);
        var conditionBody = visitor.Visit(condition.Body);

        return Expression.Lambda<Setter>(
            Expression.IfThen(
                conditionBody,
                Expression.Assign(
                    new ParameterReplacerExpressionVisitor(selector.Parameters[0], refParam).Visit(selector.Body),
                    fetch.Body
                )
            ),
            refParam,
            fetch.Parameters[0]
        ).Compile();
    }

    private static Setter MakeSetterConvertedIf<TValue, TFetch>(Expression<Func<T, TValue>> selector, Expression<Func<Snapshot, T, TFetch>> fetch, Expression<Func<T, bool>> condition)
        where TValue : unmanaged, IBinaryInteger<TValue>
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));
        ArgumentNullException.ThrowIfNull(fetch, nameof(fetch));
        var refParam = Expression.Parameter(typeof(T).MakeByRefType(), "e");

        if (selector.Body is not MemberExpression) throw new ArgumentException("Invalid selector", nameof(selector));

        var visitor = new ParameterReplacerExpressionVisitor(condition.Parameters[0], selector.Parameters[0]);
        var conditionBody = visitor.Visit(condition.Body);

        return Expression.Lambda<Setter>(
            Expression.IfThen(
                conditionBody,
                Expression.Assign(
                    new ParameterReplacerExpressionVisitor(selector.Parameters[0], refParam).Visit(selector.Body),
                    Expression.ConvertChecked(fetch.Body, typeof(TValue))
                )
            ),
            refParam,
            fetch.Parameters[0]
        ).Compile();
    }

    public void AddRef<TValue>(Expression<Func<T, TValue?>> selector)
        where TValue : DartObject
    {
        Setters.Add(MakeSetter(selector, (s, _) => s.ReadRef<TValue>()));
    }

    public void AddRefIf<TValue>(Expression<Func<T, TValue?>> selector, Expression<Func<T, bool>> condition)
        where TValue : DartObject
    {
        Setters.Add(MakeSetterIf(selector, (s, _) => s.ReadRef<TValue>(), condition));
    }

    public void AddRefId(Expression<Func<T, DartRefId>> selector)
    {
        Setters.Add(MakeSetter(selector, (s, _) => s.ReadRefId()));
    }

    public void AddRefIdIf(Expression<Func<T, DartRefId>> selector, Expression<Func<T, bool>> condition)
    {
        Setters.Add(MakeSetterIf(selector, (s, _) => s.ReadRefId(), condition));
    }

    public void AddValue<TValue>(Expression<Func<T, TValue>> selector)
        where TValue : unmanaged
    {
        Setters.Add(MakeSetter(selector, (s, _) => s.ReadValue<TValue>()));
    }

    public void AddValueIf<TValue>(Expression<Func<T, TValue>> selector, Expression<Func<T, bool>> condition)
        where TValue : unmanaged
    {
        Setters.Add(MakeSetterIf(selector, (s, _) => s.ReadValue<TValue>(), condition));
    }

    public void AddValue<TValue, TFetch>(Expression<Func<T, TValue>> selector, Expression<Func<Snapshot, T, TFetch>> fetch)
        where TValue : unmanaged, IBinaryInteger<TValue>
        where TFetch : unmanaged, IBinaryInteger<TValue>
    {
        Setters.Add(MakeSetterConverted(selector, fetch));
    }

    public void AddValueIf<TValue, TFetch>(Expression<Func<T, TValue>> selector, Expression<Func<Snapshot, T, TFetch>> fetch, Expression<Func<T, bool>> condition)
        where TValue : unmanaged, IBinaryInteger<TValue>
        where TFetch : unmanaged, IBinaryInteger<TValue>
    {
        Setters.Add(MakeSetterConvertedIf(selector, fetch, condition));
    }

    public void AddRaw<TValue>(Expression<Func<T, TValue>> selector)
        where TValue : struct
    {
        Setters.Add(MakeSetter(selector, (s, _) => s.ReadRaw<TValue>()));
    }

    public void AddRawIf<TValue>(Expression<Func<T, TValue>> selector, Expression<Func<T, bool>> condition)
        where TValue : struct
    {
        Setters.Add(MakeSetterIf(selector, (s, _) => s.ReadRaw<TValue>(), condition));
    }

    public void AddTypedData<TValue>(Expression<Func<T, TValue[]>> selector)
        where TValue : struct
    {
        Setters.Add(MakeSetter(selector, (s, _) => s.ReadTypedData<TValue>()));
    }

    public void AddTypedDataIf<TValue>(Expression<Func<T, TValue[]>> selector, Expression<Func<T, bool>> condition)
        where TValue : struct
    {
        Setters.Add(MakeSetterIf(selector, (s, _) => s.ReadTypedData<TValue>(), condition));
    }

    public void AddUnsigned<TValue>(Expression<Func<T, TValue>> selector)
        where TValue : unmanaged, IBinaryInteger<TValue>
    {
        Setters.Add(MakeSetterConverted(selector, (s, _) => s.ReadUnsigned()));
    }

    public void AddUnsignedIf<TValue>(Expression<Func<T, TValue>> selector, Expression<Func<T, bool>> condition)
        where TValue : unmanaged, IBinaryInteger<TValue>
    {
        Setters.Add(MakeSetterConvertedIf(selector, (s, _) => s.ReadUnsigned(), condition));
    }

    public void AddCid(Expression<Func<T, ClassId>> selector)
    {
        Setters.Add(MakeSetter(selector, (s, _) => s.ReadCid()));
    }
}
