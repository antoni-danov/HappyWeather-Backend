namespace WeatherWebAPI.Models
{
    public class ExternalData
    {
        public string? Location { get; set; }
        public string? WindDirection { get; set; }
        public string? ExternalLink { get; set; }
        public int WeatherIndex { get; set; }
        public string? WeatherDescription { get; set; }
    }
}
