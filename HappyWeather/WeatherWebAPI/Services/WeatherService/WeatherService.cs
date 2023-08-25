using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WeatherWebAPI.Services.WeatherService
{
    public class WeatherService: IWeatherService
    {
        private HttpClient _httpClient = new HttpClient();

        public WeatherService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient();

        }

        public async Task<HttpResponseMessage> CurrentCity(string city)
        {
            _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("BASE_URL")!.ToString());
            var response = await _httpClient.GetAsync($"current?access_key={Environment.GetEnvironmentVariable("API_KEY")}&query={city}");
            return response;
        }
    }
}
