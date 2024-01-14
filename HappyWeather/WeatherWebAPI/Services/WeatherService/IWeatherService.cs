using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI.Models.FiveDaysModel.DayModel;
using WeatherWebAPI.Models.HourlyForecastModel.HourModel;
using WeatherWebAPI.Models.RealTimeModel;
using WeatherWebAPI.Models.WeatherForecastModel;

namespace WeatherWebAPI.Services.WeatherService
{
    public interface IWeatherService
    {
        public Task<WeatherResult> GetRealTimeForecast(string cityName, string unit);
        public Task<WeatherForecast<DayUnit>> GetDailyWeatherForecast(string city, string timeStep, string unit);
        public Task<WeatherForecast<HourUnit>> GetHourlyWeatherForecast(string city, string timeStep, string unit);
    }
}
