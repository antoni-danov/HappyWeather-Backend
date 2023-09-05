namespace WeatherWebAPI.Models
{
    public class WeatherResult
    {
        public WeatherLocation? Location { get; set; }
        public CurrentWeather? Current { get; set; }
    }
}
