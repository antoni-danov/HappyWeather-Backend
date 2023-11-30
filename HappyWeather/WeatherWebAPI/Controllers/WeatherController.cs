using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private IWeatherService _service;
        public WeatherController(IWeatherService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{cityName}")]
        public async Task<ActionResult<WeatherResult>> RealTimeForecast(string cityName, string unit)
        {
            var response = new WeatherResult();
           
            try
            {
                response = await _service.GetRealTimeForecast(cityName, unit);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(response);
            }

        }

        [HttpGet]
        [Route("{cityName}/dailyforecast")]
        public async Task<ActionResult> DailyForecast(string cityName, string timeStep, string unit)
        {
            var response = new WeatherForecast<DayUnit>();

            try
            {
                response = await _service.GetDailyWeatherForecast(cityName, timeStep, unit);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("{cityName}/hourlyforecast")]
        public async Task<ActionResult> HourlyForecast(string cityName, string timeStep, string unit)
        {
            var response = new WeatherForecast<HourUnit>();

            try
            {
                response = await _service.GetHourlyWeatherForecast(cityName, timeStep, unit);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(response);
            }
        }
    }
}