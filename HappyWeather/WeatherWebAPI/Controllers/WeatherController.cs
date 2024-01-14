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
            var response = new WeatherResult();

            if (!ModelState.IsValid)
            {
                return BadRequest("The city name is required.");
            }

            try
            {
                response = await _service.GetRealTimeForecast(cityName, unit);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside RealTimeForecast action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return Ok(response);

        }

        [HttpGet]
        [Route("{cityName}/dailyforecast")]
        public async Task<ActionResult> DailyForecast(string cityName, string timeStep, string unit)
        {
            var response = new WeatherForecast<DayUnit>();

            if (!ModelState.IsValid)
            {
                return BadRequest("The city name is required.");
            }

            try
            {
                response = await _service.GetDailyWeatherForecast(cityName, timeStep, unit);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DailyForecast action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            return Ok(response);

        }

        [HttpGet]
        [Route("{cityName}/hourlyforecast")]
        public async Task<ActionResult> HourlyForecast(string cityName, string timeStep, string unit)
        {
            var response = new WeatherForecast<HourUnit>();

            if (!ModelState.IsValid)
            {
                return BadRequest("The city name is required.");
            }

            try
            {
                response = await _service.GetHourlyWeatherForecast(cityName, timeStep, unit);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside HourlyForecast action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return Ok(response);

        }
    }
}