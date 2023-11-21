namespace WeatherWebAPI.Models.WeatherForecastModel
{
    public class WeatherForecast<T>
    {
        public TimeLines<T>? TimeLines { get; set; }
        public WeatherLocation? Location { get; set; }


    }
}
