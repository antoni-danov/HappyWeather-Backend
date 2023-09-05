using Microsoft.AspNetCore.Mvc;

namespace WeatherWebAPI.Services.WeatherService
{
    public interface IWeatherService
    {
        public Task<HttpResponseMessage> CurrentCity(string cityName);
    }
}
