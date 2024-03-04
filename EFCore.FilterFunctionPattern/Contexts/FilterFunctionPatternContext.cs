using EFCore.FilterFunctionPattern.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace EFCore.FilterFunctionPattern.Contexts
{
    public class FilterFunctionPatternContext(DbContextOptions<FilterFunctionPatternContext> options) : DbContext(options)
    {
        public DbSet<WeatherForecast> Forecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
            var builder = modelBuilder.Entity<WeatherForecast>();
            builder.HasKey(x => x.Date);
            builder.HasData(Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateTime.Now.AddDays(index),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                )));
        }
    }
}
