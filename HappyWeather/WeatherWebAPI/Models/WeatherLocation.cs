using Newtonsoft.Json;

namespace WeatherWebAPI.Models
{
    public class WeatherLocation
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Region { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public string? Latitude { get; set; }

        [JsonProperty(PropertyName = "lon")]
        public string? Longitude { get; set; }

        [JsonProperty(PropertyName = "timezone_id")]
        public string? TimeZone { get; set; }
        public string? LocalTime { get; set; }
    }
}
