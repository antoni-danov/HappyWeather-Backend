using Newtonsoft.Json;

namespace WeatherWebAPI.Models.HourlyForecastModel.HourModel
{
    public class HourValues
    {
        [JsonProperty(PropertyName = "cloudCover")]
        public double CloudCoverage { get; set; }

        [JsonProperty(PropertyName = "humidity")]
        public double Humidity { get; set; }

        [JsonProperty(PropertyName = "precipitationProbability")]
        public double PrecipitationProbability { get; set; }

        [JsonProperty(PropertyName = "temperature")]
        public double Temperature { get; set; }

        [JsonProperty(PropertyName = "temperatureApparent")]
        public double ApparentTemperature { get; set; }

        [JsonProperty(PropertyName = "uvIndex")]
        public double UVIndex { get; set; }

        [JsonProperty(PropertyName = "visibility")]
        public double Visibility { get; set; }

        [JsonProperty(PropertyName = "weatherCode")]
        public WeatherCode WeatherCode { get; set; }

        [JsonProperty(PropertyName = "windDirectionAvg")]
        public double WindDirection { get; set; }

        [JsonProperty(PropertyName = "windGustMax")]
        public double MaxWindGust { get; set; }

        [JsonProperty(PropertyName = "windSpeedMax")]
        public double WindSpeed { get; set; }

    }
}