using EFCore.DbSetExtensionPattern.Context;
using EFCore.DbSetExtensionPattern.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbSetExtensionPatternContext>(opt => opt.UseInMemoryDatabase("InMemory"));
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<DbSetExtensionPatternContext>().Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", async (DbSetExtensionPatternContext context, DateTime? startAt, DateTime? endAt) =>
{
if (!startAt.HasValue)
{
startAt = DateTime.Now;
}

if (!endAt.HasValue)
{
endAt = DateTime.Now.AddDays(7);
}

var forecasts = await context.Forecasts.InRange(startAt.Value, endAt.Value).ToListAsync();
return Results.Ok(forecasts);
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();