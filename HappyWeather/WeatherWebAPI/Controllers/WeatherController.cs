using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherWebAPI.Models;
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
        public async Task<ActionResult<WeatherResult>> GetCurrentCity([FromRoute] string cityName)
        {
            var response = await _service.CurrentCity(cityName.ToLower());

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                WeatherResult finalResult = JsonConvert.DeserializeObject<WeatherResult>(result)!;
                return Ok(finalResult);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Weather API request failed.");
            }
        }
    }
}
