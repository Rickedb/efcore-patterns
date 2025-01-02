using EFCore.DbSetPattern.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DbSetPattern.Context
{
    public class WeatherForecastDbSet : DbSetProxy<WeatherForecast>
    {
        public WeatherForecastDbSet(DbSet<WeatherForecast> dbSet) : base(dbSet)
        {
        }

        public IQueryable<WeatherForecast> InRange(DateTime startAt, DateTime endAt)
        {
            return DbSet.Where(x => x.Date >= startAt && x.Date <= endAt);
        }
    }
}
