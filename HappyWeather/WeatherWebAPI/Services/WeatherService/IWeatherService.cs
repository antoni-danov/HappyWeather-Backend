using Microsoft.AspNetCore.Mvc;

namespace WeatherWebAPI.Services.WeatherService
{
    public interface IWeatherService
    {
        public Task<HttpResponseMessage> GetRealTimeForecast(string cityName, string unit);
        public Task<HttpResponseMessage> GetFiveDaysWeatherForecast(string city, string timeStep, string unit);
        public Task<HttpResponseMessage> GetHourlyWeatherForecast(string city, string timeStep, string unit);
    }
}
