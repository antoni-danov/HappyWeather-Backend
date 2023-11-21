namespace WeatherWebAPI.Models.WeatherForecastModel
{
    public class TimeLines<T>
    {
        public ICollection<T>? Daily { get; set; } = new List<T>();
        public ICollection<T>? Hourly { get; set; } = new List<T>();
    }
}
