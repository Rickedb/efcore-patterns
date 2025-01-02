using EFCore.DbSetPattern.Context;
using EFCore.DbSetPattern.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbSetPatternContext>(opt => opt.UseInMemoryDatabase("InMemory"));

// Add services to the container.

var app = builder.Build();
// Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<DbSetPatternContext>().Database.EnsureCreated();
}


app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", async (DbSetPatternContext context, DateTime? startAt, DateTime? endAt) =>
{
    if (!startAt.HasValue)
    {
        startAt = DateTime.Now;
    }

    if (!endAt.HasValue)
    {
        endAt = DateTime.Now.AddDays(7);
    }
    
    var forecasts = await context.Forecasts.Where(x => x.Date >= startAt && x.Date <= endAt).ToListAsync();
    return Results.Ok(forecasts);
});

app.Run();