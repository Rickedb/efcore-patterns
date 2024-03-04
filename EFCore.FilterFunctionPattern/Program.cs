using EFCore.FilterFunctionPattern.Contexts;
using EFCore.FilterFunctionPattern.QueryFilters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FilterFunctionPatternContext>(opt => opt.UseInMemoryDatabase("InMemory"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", async (FilterFunctionPatternContext context, DateTime? startAt, DateTime? endAt) =>
{
    if (!startAt.HasValue)
    {
        startAt = DateTime.Now;
    }

    if (!endAt.HasValue)
    {
        endAt = DateTime.Now.AddDays(7);
    }


    var filter = WeatherForecastQueryFilter.InRange(startAt.Value, endAt.Value)
                                            .And(WeatherForecastQueryFilter.ColdForecasts);
    var forecasts = await context.Forecasts.Query(filter).ToListAsync();
    return Results.Ok(forecasts);
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();