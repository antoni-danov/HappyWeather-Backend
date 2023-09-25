using Newtonsoft.Json;

namespace WeatherWebAPI.Models
{
    public class WeatherLocation
    {
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public string? Latitude { get; set; }

        [JsonProperty(PropertyName = "lon")]
        public string? Longitude { get; set; }
        public string? Type { get; set; }
    }
}
