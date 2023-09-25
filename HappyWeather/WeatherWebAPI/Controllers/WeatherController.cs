﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using WeatherWebAPI.Models;
using WeatherWebAPI.Services.WeatherService;

namespace WeatherWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private IWeatherService _service;

        public WeatherController(IMemoryCache cache, IWeatherService service)
        {
            _cache = cache;
            _service = service;
        }

        [HttpGet]
        [Route("{cityName}")]
        public async Task<ActionResult<WeatherResult>> RealTimeForecast(string cityName, string unit)
        {
            string cacheKey = $"WeatherData: {cityName}";

            if (!_cache.TryGetValue(cacheKey, out List<WeatherResult> weatherCities))
            {
                var response = await _service.GetRealTimeForecast(cityName, unit);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var finalResult = JsonConvert.DeserializeObject<WeatherResult>(result);
                    weatherCities = CacheDataByCityName(finalResult!);

                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                    _cache.Set(cacheKey, weatherCities, cacheEntryOptions);

                    return Ok(finalResult);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Weather API request failed.");
                }

            }
            return Ok(weatherCities?.FirstOrDefault(x => x.Location?.Name?.ToLower() == cityName.ToLower()!));
        }
        private List<WeatherResult> CacheDataByCityName(WeatherResult cityName)
        {
            var weatherCities = new List<WeatherResult>();
            weatherCities.Add(cityName);
            return weatherCities;
        }
    }
}
