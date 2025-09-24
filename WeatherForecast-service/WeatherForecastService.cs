using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherForecastService
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastRepository _repository;

        public WeatherForecastService(IWeatherForecastRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<WeatherForecast?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<WeatherForecast> AddAsync(WeatherForecast forecast)
        {
            return await _repository.AddAsync(forecast);
        }

        public async Task<bool> UpdateAsync(int id, WeatherForecast forecast)
        {
            forecast.Id = id;
            return await _repository.UpdateAsync(forecast);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

