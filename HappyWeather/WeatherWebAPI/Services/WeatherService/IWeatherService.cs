using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI.Models.RealTimeModel;

namespace WeatherWebAPI.Services.WeatherService
{
    public interface IWeatherService
    {
        public Task<dynamic> GetRealTimeForecast(string cityName, string unit);
        public Task<dynamic> GetDailyWeatherForecast(string city, string timeStep, string unit);
        public Task<dynamic> GetHourlyWeatherForecast(string city, string timeStep, string unit);
    }
}
