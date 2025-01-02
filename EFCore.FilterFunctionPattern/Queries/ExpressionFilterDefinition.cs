using System.Linq.Expressions;

namespace EFCore.FilterFunctionPattern.Queries
{
    public class ExpressionFilterDefinition<T> where T : class
    {
        private Expression<Func<T, bool>> _expression = x => true;

        public ExpressionFilterDefinition()
        {

        }

        private ExpressionFilterDefinition(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        public static ExpressionFilterDefinition<T> FromExpression(Expression<Func<T, bool>> expression)
        {
            return new ExpressionFilterDefinition<T>(expression);
        }

        public ExpressionFilterDefinition<T> And(Expression<Func<T, bool>> expression)
        {
            var newExpression = Expression.AndAlso(_expression.Body, expression);
            _expression = Expression.Lambda<Func<T, bool>>(newExpression, expression.Parameters);
            return this;
        }

        public ExpressionFilterDefinition<T> And(ExpressionFilterDefinition<T> definition)
        {
            var leftParameter = _expression.Parameters[0];
            var rightParameter = definition._expression.Parameters[0];

            var visitor = new ReplaceParameterVisitor(rightParameter, leftParameter);

            var leftBody = _expression.Body;
            var rightBody = visitor.Visit(definition._expression.Body);

            _expression = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(leftBody, rightBody), leftParameter);
            return this;
        }

        public ExpressionFilterDefinition<T> Or(Expression<Func<T, bool>> expression)
        {
            var invokedExpression = Expression.Invoke(expression, _expression.Parameters.Cast<Expression>());
            var newExpression = Expression.OrElse(_expression.Body, invokedExpression);
            _expression = Expression.Lambda<Func<T, bool>>(newExpression, expression.Parameters);
            return this;
        }

        public ExpressionFilterDefinition<T> Or(ExpressionFilterDefinition<T> definition)
        {
            var invokedExpression = Expression.Invoke(definition._expression, _expression.Parameters.Cast<Expression>());
            var newExpression = Expression.OrElse(_expression.Body, invokedExpression);
            _expression = Expression.Lambda<Func<T, bool>>(newExpression, definition._expression.Parameters);
            return this;
        }

        internal Expression<Func<T, bool>> Build() => _expression; 
    }

    class ReplaceParameterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameter;
        private readonly ParameterExpression _newParameter;

        public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return ReferenceEquals(node, _oldParameter) ? _newParameter : base.VisitParameter(node);
        }
    }
}
