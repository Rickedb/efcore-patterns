namespace EFCore.FilterFunctionPattern.Queries
{
    public class QueryBuilder
    {

        public QueryBuilder()
        {
            
        }

        public QueryBuilder AndEqual(string column, object value)
        {
            return this;
        }

        public QueryBuilder And(string column, object value)
        {
            return this;
        }
    }
}
