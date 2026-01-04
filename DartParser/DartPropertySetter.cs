using DartParser.Dart;
using DartParser.Dart.Objects;
using System.Linq.Expressions;
using System.Numerics;

namespace DartParser
{
    public abstract class DartPropertySetters
    {
        public static DartPropertySetters Empty { get; } = new DartPropertySetters<DartObject>();

        public abstract void FillFields<T>(T obj, Snapshot snapshot) where T : DartObject;
    }

    public class DartPropertySetters<T> : DartPropertySetters
        where T : DartObject
    {
        public List<Action<T, Snapshot>> Setters { get; } = [];

        public override void FillFields<U>(U obj, Snapshot snapshot)
        {
            if (obj is not T tobj) throw new ArgumentException("Invalid type", nameof(obj));

            foreach (var setter in Setters)
            {
                setter(tobj, snapshot);
            }
        }

        private static Action<T, Snapshot> MakeSetter<TValue>(Expression<Func<T, TValue>> selector, Expression<Func<Snapshot, TValue>> fetch)
        {
            ArgumentNullException.ThrowIfNull(selector, nameof(selector));
            ArgumentNullException.ThrowIfNull(fetch, nameof(fetch));

            if (selector.Body is not MemberExpression) throw new ArgumentException("Invalid selector", nameof(selector));

            return Expression.Lambda<Action<T, Snapshot>>(
                Expression.Assign(
                    selector.Body,
                    fetch.Body
                ),
                selector.Parameters[0],
                fetch.Parameters[0]
            ).Compile();
        }

        private class ParameterReplacerExpressionVisitor : ExpressionVisitor
        {
            public ParameterExpression SourceParameter;
            public ParameterExpression TargetParameter;

            public ParameterReplacerExpressionVisitor(ParameterExpression sourceParameter, ParameterExpression targetParameter)
            {
                SourceParameter = sourceParameter;
                TargetParameter = targetParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return ReferenceEquals(node, SourceParameter) ? TargetParameter : base.VisitParameter(node);
            }
        }

        private static Action<T, Snapshot> MakeSetterIf<TValue>(Expression<Func<T, TValue>> selector, Expression<Func<Snapshot, TValue>> fetch, Expression<Func<T, bool>> condition)
        {
            ArgumentNullException.ThrowIfNull(selector, nameof(selector));
            ArgumentNullException.ThrowIfNull(fetch, nameof(fetch));

            if (selector.Body is not MemberExpression) throw new ArgumentException("Invalid selector", nameof(selector));

            var visitor = new ParameterReplacerExpressionVisitor(condition.Parameters[0], selector.Parameters[0]);
            var conditionBody = visitor.Visit(condition.Body);

            return Expression.Lambda<Action<T, Snapshot>>(
                Expression.IfThen(
                    conditionBody,
                    Expression.Assign(
                        selector.Body,
                        fetch.Body
                    )
                ),
                selector.Parameters[0],
                fetch.Parameters[0]
            ).Compile();
        }

        public void AddRef<TValue>(Expression<Func<T, TValue?>> selector)
            where TValue : DartObject
        {
            Setters.Add(MakeSetter(selector, s => s.ReadRef<TValue>()));
        }

        public void AddRefIf<TValue>(Expression<Func<T, TValue?>> selector, Expression<Func<T, bool>> condition)
            where TValue : DartObject
        {
            Setters.Add(MakeSetterIf(selector, s => s.ReadRef<TValue>(), condition));
        }

        public void AddRefId(Expression<Func<T, DartRefId>> selector)
        {
            Setters.Add(MakeSetter(selector, s => s.ReadRefId()));
        }

        public void AddRefIdIf(Expression<Func<T, DartRefId>> selector, Expression<Func<T, bool>> condition)
        {
            Setters.Add(MakeSetterIf(selector, s => s.ReadRefId(), condition));
        }

        public void AddValue<TValue>(Expression<Func<T, TValue>> selector)
            where TValue : unmanaged
        {
            Setters.Add(MakeSetter(selector, s => s.ReadValue<TValue>()));
        }

        public void AddValueIf<TValue>(Expression<Func<T, TValue>> selector, Expression<Func<T, bool>> condition)
            where TValue : unmanaged
        {
            Setters.Add(MakeSetterIf(selector, s => s.ReadValue<TValue>(), condition));
        }

        public void AddRaw<TValue>(Expression<Func<T, TValue>> selector)
            where TValue : struct
        {
            Setters.Add(MakeSetter(selector, s => s.ReadRaw<TValue>()));
        }

        public void AddRawIf<TValue>(Expression<Func<T, TValue>> selector, Expression<Func<T, bool>> condition)
            where TValue : struct
        {
            Setters.Add(MakeSetterIf(selector, s => s.ReadRaw<TValue>(), condition));
        }

        public void AddTypedData<TValue>(Expression<Func<T, TValue[]>> selector)
            where TValue : struct
        {
            Setters.Add(MakeSetter(selector, s => s.ReadTypedData<TValue>()));
        }

        public void AddTypedDataIf<TValue>(Expression<Func<T, TValue[]>> selector, Expression<Func<T, bool>> condition)
            where TValue : struct
        {
            Setters.Add(MakeSetterIf(selector, s => s.ReadTypedData<TValue>(), condition));
        }

        public void AddUnsigned(Expression<Func<T, ulong>> selector)
        {
            Setters.Add(MakeSetter(selector, s => s.ReadUnsigned()));
        }

        public void AddUnsignedIf(Expression<Func<T, ulong>> selector, Expression<Func<T, bool>> condition)
        {
            Setters.Add(MakeSetterIf(selector, s => s.ReadUnsigned(), condition));
        }

        public void AddCid(Expression<Func<T, ClassId>> selector)
        {
            Setters.Add(MakeSetter(selector, s => s.ReadCid()));
        }
    }
}
