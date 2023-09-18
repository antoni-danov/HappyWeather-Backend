using Newtonsoft.Json;

namespace WeatherWebAPI.Models
{
    public class CurrentWeather
    {
        [JsonProperty(PropertyName = "observation_time")]
        public string? ObservationTime { get; set; }
        public int Temperature { get; set; }

        [JsonProperty(PropertyName = "weather_icons")]
        public string[]? WeatherIcon { get; set; } // TODO

        [JsonProperty(PropertyName = "weather_descriptions")]
        public string[]? WeatherDescription { get; set; } // TODO
        public int WindSpeed { get; set; }

        [JsonProperty(PropertyName = "wind_degree")]
        public int WindDegree { get; set; }

        [JsonProperty(PropertyName = "wind_dir")]
        public string? WindDirection { get; set; }
        public int Pressure { get; set; }
        public int Precipitations { get; set; }
        public int Humidity { get; set; }
        public int CloudCover { get; set; }
        public int UVIndex { get; set; }
        public int Visibility { get; set; }
        [JsonProperty(PropertyName ="is_day")]
        public string? IsDay { get; set; }
    }
}
