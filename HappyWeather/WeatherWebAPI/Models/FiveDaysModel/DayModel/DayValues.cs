using Newtonsoft.Json;

namespace WeatherWebAPI.Models.FiveDaysModel.DayModel
{
    public class DayValues
    {
        [JsonProperty(PropertyName = "cloudCoverAvg")]
        public double CloudCoverAverage { get; set; }

        [JsonProperty(PropertyName = "humidityAvg")]
        public double AverageHumidity { get; set; }
        
        [JsonProperty(PropertyName = "moonriseTime")]
        public string? MoonRise { get; set; }
        
        [JsonProperty(PropertyName = "moonsetTime")]
        public string? MoonSet { get; set; }
        
        [JsonProperty(PropertyName = "precipitationProbabilityAvg")]
        public double PrecipitationProbability { get; set; }
        
        [JsonProperty(PropertyName = "sunriseTime")]
        public string? Sunrise { get; set; }  
        
        [JsonProperty(PropertyName = "sunsetTime")]
        public string? Sunset { get; set; }
        
        [JsonProperty(PropertyName = "temperatureApparentAvg")]
        public double ApparentTemperature { get; set; }
        
        [JsonProperty(PropertyName = "temperatureMax")]
        public double MaxTemperature { get; set; }
        
        [JsonProperty(PropertyName = "temperatureMin")]
        public double MinTemperature { get; set; }
        
        [JsonProperty(PropertyName = "uvIndexAvg")]
        public double AverageUVIndex { get; set; }
        
        [JsonProperty(PropertyName = "visibilityAvg")]
        public double Visibility { get; set; }
        
        [JsonProperty(PropertyName = "weatherCodeMax")]
        public WeatherCode WeatherCode { get; set; }
        
        [JsonProperty(PropertyName = "windDirectionAvg")]
        public double WindDirection { get; set; }
        
        [JsonProperty(PropertyName = "windGustMax")]
        public double MaxWindGust { get; set; }
        
        [JsonProperty(PropertyName = "windSpeedMax")]
        public double WindSpeed { get; set; }
    }
}
