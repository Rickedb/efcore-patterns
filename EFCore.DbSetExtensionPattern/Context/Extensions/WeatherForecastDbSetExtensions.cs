using EFCore.DbSetExtensionPattern.Entities;

namespace Microsoft.EntityFrameworkCore
{
    public static class WeatherForecastDbSetExtensions
    {
        public static IQueryable<WeatherForecast> InRange(this DbSet<WeatherForecast> dbSet, DateTime startAt, DateTime endAt)
        {
            return dbSet.Where(x => x.Date >= startAt && x.Date <= endAt);
        }

        public static async Task<IEnumerable<WeatherForecast>> ColdForecastsAsync(this DbSet<WeatherForecast> dbSet)
        {
            return await dbSet.Where(x => x.CelsiusTemperature < 20).ToListAsync();
        }

        public static async Task<IEnumerable<WeatherForecast>> NormalForecastsAsync(this DbSet<WeatherForecast> dbSet)
        {
            return await dbSet.Where(x => x.CelsiusTemperature >= 20 && x.CelsiusTemperature <= 25).ToListAsync();
        }

        public static async Task<IEnumerable<WeatherForecast>> HotForecastsAsync(this DbSet<WeatherForecast> dbSet)
        {
            return await dbSet.Where(x => x.CelsiusTemperature > 25).ToListAsync();
        }
    }
}
