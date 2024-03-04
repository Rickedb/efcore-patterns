using System.Linq.Expressions;

namespace EFCore.FilterFunctionPattern.Queries
{
    public class QueryFilterDefinition<T> where T : class
    {
        private Expression<Func<T, bool>> _expression = x => true;

        public QueryFilterDefinition()
        {

        }

        private QueryFilterDefinition(Expression<Func<T, bool>> expression)
        {
            _expression = expression;
        }

        public static QueryFilterDefinition<T> FromExpression(Expression<Func<T, bool>> expression)
        {
            return new QueryFilterDefinition<T>(expression);
        }

        public QueryFilterDefinition<T> And(Expression<Func<T, bool>> expression)
        {
            var invokedExpression = Expression.Invoke(expression, _expression.Parameters.Cast<Expression>());
            var newExpression = Expression.AndAlso(_expression.Body, invokedExpression);
            _expression = Expression.Lambda<Func<T, bool>>(newExpression, expression.Parameters);
            return this;
        }

        public QueryFilterDefinition<T> And(QueryFilterDefinition<T> definition)
        {
            var invokedExpression = Expression.Invoke(definition._expression, _expression.Parameters.Cast<Expression>());
            var newExpression = Expression.AndAlso(_expression.Body, invokedExpression);
            _expression = Expression.Lambda<Func<T, bool>>(newExpression, definition._expression.Parameters);
            return this;
        }

        public QueryFilterDefinition<T> Or(Expression<Func<T, bool>> expression)
        {
            var invokedExpression = Expression.Invoke(expression, _expression.Parameters.Cast<Expression>());
            var newExpression = Expression.OrElse(_expression.Body, invokedExpression);
            _expression = Expression.Lambda<Func<T, bool>>(newExpression, expression.Parameters);
            return this;
        }

        public QueryFilterDefinition<T> Or(QueryFilterDefinition<T> definition)
        {
            var invokedExpression = Expression.Invoke(definition._expression, _expression.Parameters.Cast<Expression>());
            var newExpression = Expression.OrElse(_expression.Body, invokedExpression);
            _expression = Expression.Lambda<Func<T, bool>>(newExpression, definition._expression.Parameters);
            return this;
        }

        internal Expression<Func<T, bool>> Build() => _expression; 
    }
}
