using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WeatherWebAPI.Services.WeatherService
{
    public class WeatherService: IWeatherService
    {
        private HttpClient client;
        public WeatherService()
        {
            this.client = new HttpClient();
        }

        public async Task<HttpResponseMessage> CurrentCity(string city)
        {
            
            var response = await this.client.GetAsync($"{Environment.GetEnvironmentVariable("BASE_URL")}current?access_key={Environment.GetEnvironmentVariable("API_KEY")}&query={city}");

            return response;
        }
    }
}
