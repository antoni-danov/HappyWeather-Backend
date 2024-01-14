namespace WeatherWebAPI.Tests.Fixtures
{
    internal class TestVariables
    {
        public string cityName { get; set; } = "London";
        public string invalidCity { get; set; } = "InvalidCityName";
        public string unit { get; set; } = "metric";
        public string dailyStep { get; set; } = "1d";
        public string hourlyStep { get; set; } = "1h";
    }
}
