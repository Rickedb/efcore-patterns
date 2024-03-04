using EFCore.RepositoryPattern.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.RepositoryPattern.Context
{
    public class RepositoryPatternContext(DbContextOptions<RepositoryPatternContext> options) : DbContext(options)
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
