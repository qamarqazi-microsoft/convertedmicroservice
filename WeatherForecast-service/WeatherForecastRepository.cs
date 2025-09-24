using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeatherForecastService
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly WeatherForecastDbContext _context;

        public WeatherForecastRepository(WeatherForecastDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
        {
            return await _context.WeatherForecasts.ToListAsync();
        }

        public async Task<WeatherForecast?> GetByIdAsync(int id)
        {
            return await _context.WeatherForecasts.FindAsync(id);
        }

        public async Task<WeatherForecast> AddAsync(WeatherForecast forecast)
        {
            _context.WeatherForecasts.Add(forecast);
            await _context.SaveChangesAsync();
            return forecast;
        }

        public async Task<bool> UpdateAsync(WeatherForecast forecast)
        {
            var existing = await _context.WeatherForecasts.FindAsync(forecast.Id);
            if (existing == null) return false;

            existing.Date = forecast.Date;
            existing.TemperatureC = forecast.TemperatureC;
            existing.Summary = forecast.Summary;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.WeatherForecasts.FindAsync(id);
            if (existing == null) return false;

            _context.WeatherForecasts.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

