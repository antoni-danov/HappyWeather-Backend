namespace WeatherWebAPI.Models.RealTimeModel
{
    public class WeatherValues
    {
        public double? CloudBase { get; set; }
        public double? CloudCeiling { get; set; }
        public int CloudCover { get; set; }
        public int Humidity { get; set; }
        public int PrecipitationProbability { get; set; }
        public string? RainIntensity { get; set; }
        public string? SnowIntensity { get; set; }
        public double Temperature { get; set; }
        public double TemperatureApparent { get; set; }
        public int UvIndex { get; set; }
        public double Visibility { get; set; }
        public WeatherCodeFullDay WeatherCode { get; set; }
        public double WindDirection { get; set; }
        public double WindGust { get; set; }
        public double WindSpeed { get; set; }
    }
}
