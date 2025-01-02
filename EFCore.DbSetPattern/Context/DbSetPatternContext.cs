using EFCore.DbSetPattern.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DbSetPattern.Context
{
    public class DbSetPatternContext : DbContext
    {
        private Lazy<WeatherForecastDbSet> _dbset;

        public WeatherForecastDbSet Forecasts => _dbset.Value;

        public DbSetPatternContext(DbContextOptions<DbSetPatternContext> options) : base(options)
        {
            _dbset = new Lazy<WeatherForecastDbSet>(() => new WeatherForecastDbSet(Set<WeatherForecast>()));
        }

       
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
