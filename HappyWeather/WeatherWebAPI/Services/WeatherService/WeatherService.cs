using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using WeatherWebAPI.Models.FiveDaysModel.DayModel;
using WeatherWebAPI.Models.HourlyForecastModel.HourModel;
using WeatherWebAPI.Models.RealTimeModel;
using WeatherWebAPI.Models.WeatherForecastModel;

namespace WeatherWebAPI.Services.WeatherService
{
    public class WeatherService : IWeatherService
    {
        private readonly ILogger<WeatherService> _logger;
        private readonly IMemoryCache _cache;
        private readonly HttpClient _client;
        public WeatherService(HttpClient client, IMemoryCache cache, ILogger<WeatherService> logger)
        {
            _cache = cache;
            _client = client;
            _logger = logger;
        }

        public async Task<WeatherResult> GetRealTimeForecast(string city, string unit)
        {
            // create cache key
            string cacheKey = $"{city}";

            //check if the cahche key already exists
            if (_cache.TryGetValue(cacheKey, out Dictionary<string, WeatherResult>? weatherCities))
            {
                return weatherCities![city];
            }

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{Environment.GetEnvironmentVariable("TOMORROW_BASE_URL")}weather/realtime?location={city}&language=en-US&units={unit}&apikey={Environment.GetEnvironmentVariable("TOMORROW_API_KEY")}");
                //if is successful transform the result
                response.EnsureSuccessStatusCode();
                var finalResult = TransformData<WeatherResult>(response);
                //evoque the CacheDataByCityName method and return Dictionnary collection with key, value pair 
                weatherCities = CacheData<WeatherResult>(cacheKey, finalResult.Result!);
                //Set expiration time for the memory cache data
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                //Set the cache
                _cache.Set(cacheKey, weatherCities, cacheEntryOptions);
                return finalResult.Result;

            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Could not perform http request for GetRealTimeForecast.");
                throw;
            }
        }

        public async Task<WeatherForecast<DayUnit>> GetDailyWeatherForecast(string city, string timeStep, string unit)
        {
            string cacheKey = $"Five days: {city}";
            if (_cache.TryGetValue(cacheKey, out Dictionary<string, WeatherForecast<DayUnit>>? fiveDaysForecast))
            {
                return fiveDaysForecast![cacheKey];
            }

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{Environment.GetEnvironmentVariable("TOMORROW_BASE_URL")}/weather/forecast?location={city}&timesteps={timeStep}&units={unit}&apikey={Environment.GetEnvironmentVariable("TOMORROW_API_KEY")}");

                //if is successful transform the result
                response.EnsureSuccessStatusCode();
                var finalResult = TransformData<WeatherForecast<DayUnit>>(response);
                //evoque the CacheDataByCityName method and return Dictionnary collection with key, value pair 
                fiveDaysForecast = CacheData<WeatherForecast<DayUnit>>(cacheKey, finalResult.Result!);
                //Set expiration time for the memory cache data
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                //Set the cache
                _cache.Set(cacheKey, fiveDaysForecast, cacheEntryOptions);
                return finalResult.Result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Could not perform http request for GetDailyWeatherForecast");
                throw;
            }
        }

        public async Task<WeatherForecast<HourUnit>> GetHourlyWeatherForecast(string city, string timeStep, string unit)
        {
            string cacheKey = $"Hourly: {city}";

            if (_cache.TryGetValue(cacheKey, out Dictionary<string, WeatherForecast<HourUnit>>? hourlyForecast))
            {
                return hourlyForecast![cacheKey];
            }

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{Environment.GetEnvironmentVariable("TOMORROW_BASE_URL")}/weather/forecast?location={city}&timesteps={timeStep}&units={unit}&apikey={Environment.GetEnvironmentVariable("TOMORROW_API_KEY")}");

                //if is successful transform the result
                response.EnsureSuccessStatusCode();
                var finalResult = TransformData<WeatherForecast<HourUnit>>(response);
                //evoque the CacheDataByCityName method and return Dictionnary collection with key, value pair 
                hourlyForecast = CacheData<WeatherForecast<HourUnit>>(cacheKey, finalResult.Result!);
                //Set expiration time for the memory cache data
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                //Set the cache
                _cache.Set(cacheKey, hourlyForecast, cacheEntryOptions);
                return finalResult.Result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Could not perform http request for GetHourlyWeatherForecast");
                throw;
            }
        }

        private async Task<T> TransformData<T>(HttpResponseMessage data)
        {
            Type typeValue = typeof(T);
            var result = Activator.CreateInstance<T>();

            //get the result as string
            string transformedData = await data.Content.ReadAsStringAsync();

            if (typeValue.Name == typeof(WeatherResult).Name)
            {
                //deserialize it to custom object
                result = JsonConvert.DeserializeObject<T>(transformedData)!;
            }
            else if (typeValue.Name == typeof(WeatherForecast<HourUnit>).Name)
            {
                //deserialize it to custom object
                result = JsonConvert.DeserializeObject<T>(transformedData)!;
            }
            else if (typeValue.Name == typeof(WeatherForecast<DayUnit>).Name)
            {
                //deserialize it to custom object
                result = JsonConvert.DeserializeObject<T>(transformedData)!;
                return result;
            }

            return result;

        }

        private Dictionary<string, T> CacheData<T>(string key, dynamic data)
        {
            var cachedCollection = new Dictionary<string, T>();
            cachedCollection.Add(key, data);
            return cachedCollection;
        }
    }
}
