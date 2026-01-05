using System.Linq.Expressions;

namespace DartParser;

public class ParameterReplacerExpressionVisitor : ExpressionVisitor
{
    public ParameterExpression SourceParameter;
    public Expression TargetParameter;

    public ParameterReplacerExpressionVisitor(ParameterExpression sourceParameter, Expression targetParameter)
    {
        SourceParameter = sourceParameter;
        TargetParameter = targetParameter;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return ReferenceEquals(node, SourceParameter) ? TargetParameter : base.VisitParameter(node);
    }
}

