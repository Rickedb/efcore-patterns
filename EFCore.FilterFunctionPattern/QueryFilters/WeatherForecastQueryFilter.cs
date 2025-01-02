using EFCore.FilterFunctionPattern.Entities;
using EFCore.FilterFunctionPattern.Queries;

namespace EFCore.FilterFunctionPattern.QueryFilters
{
    public class WeatherForecastQueryFilter
    {
        public static ExpressionFilterDefinition<WeatherForecast> ColdForecasts => ExpressionFilterDefinition<WeatherForecast>.FromExpression(x => x.CelsiusTemperature < 20);
        public static ExpressionFilterDefinition<WeatherForecast> NormalForecasts => ExpressionFilterDefinition<WeatherForecast>.FromExpression(x => x.CelsiusTemperature < 20);
        public static ExpressionFilterDefinition<WeatherForecast> HotForecasts => ExpressionFilterDefinition<WeatherForecast>.FromExpression(x => x.CelsiusTemperature > 25);

        public static ExpressionFilterDefinition<WeatherForecast> InRange(DateTime startAt, DateTime endAt)
            => ExpressionFilterDefinition<WeatherForecast>.FromExpression(x=> x.Date >= startAt && x.Date <= endAt);
    }
}
