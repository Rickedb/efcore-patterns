using EFCore.FilterFunctionPattern.Entities;
using EFCore.FilterFunctionPattern.Queries;

namespace EFCore.FilterFunctionPattern.QueryFilters
{
    public class WeatherForecastQueryFilter
    {
        public static QueryFilterDefinition<WeatherForecast> ColdForecasts => QueryFilterDefinition<WeatherForecast>.FromExpression(x => x.CelsiusTemperature < 20);
        public static QueryFilterDefinition<WeatherForecast> NormalForecasts => QueryFilterDefinition<WeatherForecast>.FromExpression(x => x.CelsiusTemperature < 20);
        public static QueryFilterDefinition<WeatherForecast> HotForecasts => QueryFilterDefinition<WeatherForecast>.FromExpression(x => x.CelsiusTemperature > 25);

        public static QueryFilterDefinition<WeatherForecast> InRange(DateTime startAt, DateTime endAt)
            => QueryFilterDefinition<WeatherForecast>.FromExpression(x=> x.Date >= startAt && x.Date <= endAt);
    }
}
