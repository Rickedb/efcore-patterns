using EFCore.FilterFunctionPattern.Queries;

namespace Microsoft.EntityFrameworkCore
{
    public static class DbSetExtensions
    {
        public static IQueryable<T> Query<T>(this DbSet<T> dbSet, QueryFilterDefinition<T> filter) where T : class
        {
            var expression = filter.Build();
            return dbSet.Where(expression);
        }

    }
}
