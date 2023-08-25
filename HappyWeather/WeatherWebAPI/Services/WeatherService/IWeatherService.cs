using Microsoft.AspNetCore.Mvc;

namespace WeatherWebAPI.Services.WeatherService
{
    public interface IWeatherService
    {
        Task<HttpResponseMessage> CurrentCity(string city);
    }
}
