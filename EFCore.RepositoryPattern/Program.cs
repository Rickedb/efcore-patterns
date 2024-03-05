using EFCore.RepositoryPattern.Context;
using EFCore.RepositoryPattern.Entities;
using EFCore.RepositoryPattern.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RepositoryPatternContext>(opt => opt.UseInMemoryDatabase("InMemory"));
builder.Services.AddScoped<WeatherForecastRepository>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<RepositoryPatternContext>().Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", async (WeatherForecastRepository repository, DateTime? startAt, DateTime? endAt) =>
{
    if (!startAt.HasValue)
    {
        startAt = DateTime.Now;
    }

    if (!endAt.HasValue)
    {
        endAt = DateTime.Now.AddDays(7);
    }

    var forecasts = await repository.InRangeAsync(startAt.Value, endAt.Value);
    return Results.Ok(forecasts);
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();
