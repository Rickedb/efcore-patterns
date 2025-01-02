using EFCore.RepositoryPattern.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EFCore.RepositoryPattern.Context
{
    public class LazyLoadedRepositoryPatternContext : DbContext
    {
        private readonly Lazy<LazyWeatherForecastRepository> _repository;

        public LazyWeatherForecastRepository Forecasts => _repository.Value;

        public LazyLoadedRepositoryPatternContext(DbContextOptions<LazyLoadedRepositoryPatternContext> options) : base(options)
        {
            _repository = new Lazy<LazyWeatherForecastRepository>(() => new LazyWeatherForecastRepository(this));
        }
    }
}
