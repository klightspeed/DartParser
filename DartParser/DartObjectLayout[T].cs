using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;

namespace DartParser;

public class DartObjectLayout<T>(bool is64Bit, bool isBigEndian) : DartObjectLayout
{
    public bool Is64Bit { get; } = is64Bit;
    public bool IsBigEndian { get; } = isBigEndian;
    public bool IsOppositeEndian => IsBigEndian == BitConverter.IsLittleEndian;

    public delegate void Setter(ref T obj, Snapshot snapshot, DartStream stream);

    public delegate TValue Fetcher<TValue>(DartStream stream, Snapshot snapshot, int align);

    public List<Setter> Setters { get; } = [];

    public override void FillFields<U>(U obj, Snapshot snapshot, DartStream stream)
    {
        if (obj is not T tobj) throw new ArgumentException("Invalid type", nameof(obj));

        foreach (var setter in Setters)
        {
            setter(ref tobj, snapshot, stream);
        }
    }

    public override void FillFields<U>(ref U obj, Snapshot snapshot, DartStream stream)
    {
        if (obj is not T tobj) throw new ArgumentException("Invalid type", nameof(obj));

        foreach (var setter in Setters)
        {
            setter(ref tobj, snapshot, stream);
        }
    }

    private static Setter MakeSetter(
            Expression memberExpr,
            ParameterExpression selParam,
            Expression fetchExpr,
            ParameterExpression snapshotParam,
            ParameterExpression streamParam,
            ParameterExpression alignParam,
            int align
        )
    {
        switch (memberExpr)
        {
            case not MemberExpression:
            case MemberExpression { Member: not PropertyInfo and not FieldInfo }:
                throw new ArgumentException("Must be a property or field accessor", nameof(memberExpr));
            case MemberExpression { Member: PropertyInfo { CanWrite: false } }:
                throw new ArgumentException("Property must have a setter", nameof(memberExpr));
            case MemberExpression { Member: FieldInfo { IsInitOnly: true } }:
                throw new ArgumentException("Field must not be readonly", nameof(memberExpr));
        }

        var refParam = Expression.Parameter(typeof(T).MakeByRefType(), "e");
        memberExpr = new ParameterReplacerExpressionVisitor(selParam, refParam).Visit(memberExpr);
        fetchExpr = new ParameterReplacerExpressionVisitor(alignParam, Expression.Constant(align)).Visit(fetchExpr);

        return Expression.Lambda<Setter>(
            Expression.Assign(
                memberExpr,
                fetchExpr
            ),
            refParam,
            snapshotParam,
            streamParam
        ).Compile();
    }

    private static Setter MakeSetter<TValue>(Expression<Func<T, TValue>> selector, Expression<Fetcher<TValue>> fetch, int align = -1)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));
        ArgumentNullException.ThrowIfNull(fetch, nameof(fetch));
        if (selector.Body is not MemberExpression memberExpr) throw new ArgumentException("Must be a property or field accessor", nameof(selector));
        return MakeSetter(
            memberExpr,
            selector.Parameters[0],
            fetch.Body,
            fetch.Parameters[0],
            fetch.Parameters[1],
            fetch.Parameters[2],
            align
        );
    }

    private static Setter MakeSetterConverted<TValue, TFetch>(Expression<Func<T, TValue>> selector, Expression<Fetcher<TFetch>> fetch, int align = -1)
        where TValue : unmanaged, IBinaryInteger<TValue>
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));
        ArgumentNullException.ThrowIfNull(fetch, nameof(fetch));
        if (selector.Body is not MemberExpression memberExpr) throw new ArgumentException("Must be a property accessor", nameof(selector));
        return MakeSetter(
            memberExpr,
            selector.Parameters[0],
            Expression.ConvertChecked(fetch.Body, typeof(TValue)),
            fetch.Parameters[0],
            fetch.Parameters[1],
            fetch.Parameters[2],
            align
        );
    }
}
