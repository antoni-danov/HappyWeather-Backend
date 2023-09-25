namespace WeatherWebAPI.Models
{
    public class WeatherValues
    {
        //Kilometers
        public double? CloudBase { get; set; }

        //Kilometers
        public double? CloudCeiling { get; set; }

        //Pourcentage
        public int CloudCover { get; set; }
        public int Humidity { get; set; }
        
        //Pourcentage
        public int PrecipitationProbability { get; set; }
        public string? RainIntensity { get; set; }
        public string? SnowIntensity { get; set; }
        public double Temperature { get; set; }
        public double TemperatureApparent { get; set; }
        public int UvIndex { get; set; }
        
        //Kilometers
        public double Visibility { get; set; }
        public WeatherCodeFullDay WeatherCode { get; set; }

        //Degree
        public double WindDirection { get; set; }

        //Meters per second
        public double WindGust { get; set; }
        public double WindSpeed { get; set; }
    }
}
