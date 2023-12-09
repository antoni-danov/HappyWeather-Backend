using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WeatherWebAPI.Models.RealTimeModel
{
    public class WeatherData
    {
        [JsonProperty(PropertyName = "time")]
        public string? Time { get; set; }

        [JsonProperty(PropertyName = "values")]
        public WeatherValues? Values { get; set; }
    }
}
