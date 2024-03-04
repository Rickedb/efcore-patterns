namespace EFCore.FilterFunctionPattern.Entities
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int CelsiusTemperature { get; set; }
        public string? Summary { get; set; }

        public WeatherForecast()
        {

        }

        public WeatherForecast(DateTime date, int temperatureC, string? summary)
        {
            Date = date;
            CelsiusTemperature = temperatureC;
            Summary = summary;
        }
    }
}
