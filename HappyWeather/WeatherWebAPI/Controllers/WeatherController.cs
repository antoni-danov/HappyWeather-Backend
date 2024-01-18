using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WeatherWebAPI.Models.FiveDaysModel.DayModel;
using WeatherWebAPI.Models.HourlyForecastModel.HourModel;
using WeatherWebAPI.Models.RealTimeModel;
using WeatherWebAPI.Models.WeatherForecastModel;
using WeatherWebAPI.Services.WeatherService;

namespace WeatherWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private ILogger<WeatherController> _logger;
        private IWeatherService _service;
        public WeatherController(ILogger<WeatherController> logger, IWeatherService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("{cityName}")]
        public async Task<ActionResult> RealTimeForecast(string cityName, string unit)
        {
            if (String.IsNullOrEmpty(cityName))
            {
                return BadRequest("City name is required.");
            }
            if (String.IsNullOrEmpty(unit))
            {
                return BadRequest("Unit field is required.");
            }

            var response = await _service.GetRealTimeForecast(cityName, unit);

            return Ok(response);

        }

        [HttpGet]
        [Route("{cityName}/dailyforecast")]
        public async Task<ActionResult> DailyForecast(string cityName, string timeStep, string unit)
        {
            if (String.IsNullOrEmpty(cityName))
            {
                return BadRequest("City name is required.");
            }
            if (String.IsNullOrEmpty(unit))
            {
                return BadRequest("Unit field is required.");
            }
            if (String.IsNullOrEmpty(timeStep))
            {
                return BadRequest("Timestep field is required.");
            }

            var response = await _service.GetDailyWeatherForecast(cityName, timeStep, unit);
            return Ok(response);

        }

        [HttpGet]
        [Route("{cityName}/hourlyforecast")]
        public async Task<ActionResult> HourlyForecast(string cityName, string timeStep, string unit)
        {
            if (String.IsNullOrEmpty(cityName))
            {
                return BadRequest("City name is required.");
            }
            if (String.IsNullOrEmpty(unit))
            {
                return BadRequest("Unit field is required.");
            }
            if (String.IsNullOrEmpty(timeStep))
            {
                return BadRequest("Timestep field is required.");
            }

            var response = await _service.GetHourlyWeatherForecast(cityName, timeStep, unit);

            return Ok(response);
        }
    }
}