using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        public async Task<IActionResult> GetCurrentCity()
        {
            var response = await _service.CurrentCity("SoFia".ToLower());

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Weather API request failed.");
            }
        }
    }
}
