﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<HttpResponseMessage> GetRealTimeForecast(string city, string unit) => await this.client.GetAsync($"{Environment.GetEnvironmentVariable("TOMORROW_BASE_URL")}weather/realtime?location={city}&language=en-US&units={unit}&apikey={Environment.GetEnvironmentVariable("TOMORROW_API_KEY")}");
        
    }
}
