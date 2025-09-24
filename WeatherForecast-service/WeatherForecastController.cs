using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherForecastService
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _service;

        public WeatherForecastController(IWeatherForecastService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetAll()
        {
            var forecasts = await _service.GetAllAsync();
            return Ok(forecasts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecast>> GetById(int id)
        {
            var forecast = await _service.GetByIdAsync(id);
            if (forecast == null)
                return NotFound();
            return Ok(forecast);
        }

        [HttpPost]
        public async Task<ActionResult<WeatherForecast>> Create([FromBody] WeatherForecast forecast)
        {
            var created = await _service.AddAsync(forecast);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WeatherForecast forecast)
        {
            var updated = await _service.UpdateAsync(id, forecast);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}

