using EFCore.RepositoryPattern.Context;
using EFCore.RepositoryPattern.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.RepositoryPattern.Repositories
{
    public class WeatherForecastRepository(RepositoryPatternContext context)
    {
        public async Task<IEnumerable<WeatherForecast>> InRangeAsync(DateTime startAt, DateTime endAt)
        {
            return await context.Forecasts.Where(x => x.Date >= startAt && x.Date <= endAt).ToListAsync();
        }

        public async Task<IEnumerable<WeatherForecast>> ColdForecastsAsync()
        {
            return await context.Forecasts.Where(x => x.CelsiusTemperature < 20).ToListAsync();
        }

        public async Task<IEnumerable<WeatherForecast>> NormalForecastsAsync()
        {
            return await context.Forecasts.Where(x => x.CelsiusTemperature >= 20 && x.CelsiusTemperature <= 25).ToListAsync();
        }

        public async Task<IEnumerable<WeatherForecast>> HotForecastsAsync()
        {
            return await context.Forecasts.Where(x => x.CelsiusTemperature > 25).ToListAsync();
        }
    }
}
