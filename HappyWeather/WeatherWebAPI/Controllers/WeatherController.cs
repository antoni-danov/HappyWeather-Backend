using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
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
            // create cache key
            string cacheKey = $"{cityName}";

            //check if the cahche key already exists
            if (!_cache.TryGetValue(cacheKey, out Dictionary<string, WeatherResult>? weatherCities))
            {
                //if the cache key is not found evoque RealTimeForecast mehod in the services
                var response = await _service.GetRealTimeForecast(cityName, unit);

                //if is successful transform the result
                if (response.IsSuccessStatusCode)
                {
                    //get the result as string
                    var result = await response.Content.ReadAsStringAsync();
                    //deserialize it to custom object
                    dynamic finalResult = JsonConvert.DeserializeObject<WeatherResult>(result)!;
                    //evoque the CacheDataByCityName method and return Dictionnary collection with key, value pair 
                    weatherCities = CacheDataByCityName(cacheKey, finalResult!);
                    //Set expiration time for the memory cache data
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                    //Set the cache
                    _cache.Set(cacheKey, weatherCities, cacheEntryOptions);
                    //return the result
                    return Ok(finalResult);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Weather API request failed.");
                }
            }
            return Ok(weatherCities?[cityName]);
        }

        [HttpGet]
        [Route("{cityName}/dailyforecast")]
        public async Task<ActionResult> DailyForecast(string cityName, string timeStep, string unit)
        {
            string cacheKey = $"Five days: {cityName}";

            if (!_cache.TryGetValue(cacheKey, out Dictionary<string, WeatherForecast<DayUnit>>? fiveDaysForecast))
            {
                var response = await _service.GetDailyWeatherForecast(cityName, timeStep, unit);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var finalResult = JsonConvert.DeserializeObject<WeatherForecast<DayUnit>>(result)!;
                    finalResult.TimeLines.Daily = finalResult.TimeLines?.Daily?.Skip(1).ToList();
                    fiveDaysForecast = DailyForecastCachedData(cacheKey, finalResult!);

                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                    _cache.Set(cacheKey, fiveDaysForecast, cacheEntryOptions);

                    return Ok(finalResult);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Weather API request failed.");
                }

            }
            return Ok(fiveDaysForecast?[cacheKey]);
        }

        [HttpGet]
        [Route("{cityName}/hourlyforecast")]
        public async Task<ActionResult> HourlyForecast(string cityName, string timeStep, string unit)
        {
            string cacheKey = $"Hourly: {cityName}";

            if (!_cache.TryGetValue(cacheKey, out Dictionary<string, WeatherForecast<HourUnit>>? hourlyForecast))
            {
                var response = await _service.GetHourlyWeatherForecast(cityName, timeStep, unit);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var finalResult = JsonConvert.DeserializeObject<WeatherForecast<HourUnit>>(result)!;
                    finalResult.TimeLines.Hourly = finalResult.TimeLines?.Hourly?.Take(25).ToList();
                    hourlyForecast = HourlyForecastCachedData(cacheKey, finalResult!);
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                    _cache.Set(cacheKey, hourlyForecast, cacheEntryOptions);

                    return Ok(finalResult);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Weather API request failed.");
                }

            }
            return Ok(hourlyForecast?[cacheKey]);

        }
        private Dictionary<string, WeatherResult> CacheDataByCityName(string key, dynamic cityName)
        {
            var weatherCities = new Dictionary<string, WeatherResult>();
            weatherCities.Add(key, cityName);
            return weatherCities;
        }
        private Dictionary<string, WeatherForecast<DayUnit>> DailyForecastCachedData(string key, WeatherForecast<DayUnit> cityData)
        {
            var fiveDaysForecast = new Dictionary<string, WeatherForecast<DayUnit>>();
            fiveDaysForecast.Add(key, cityData);
            return fiveDaysForecast;
        }
        private Dictionary<string, WeatherForecast<HourUnit>> HourlyForecastCachedData(string key, WeatherForecast<HourUnit> cityData)
        {
            var hourlyForecast = new Dictionary<string, WeatherForecast<HourUnit>>();
            hourlyForecast.Add(key, cityData);
            return hourlyForecast;
        }

    }
}