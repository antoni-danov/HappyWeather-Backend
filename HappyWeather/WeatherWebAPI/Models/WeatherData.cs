using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WeatherWebAPI.Models
{
    public class WeatherData
    {
        [JsonProperty(PropertyName="time")]
        public string WeatherDateTime { get; set; }

        [JsonProperty(PropertyName="values")]
        public WeatherValues? Values { get; set; }
    }
}
