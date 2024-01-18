namespace WeatherWebAPI.Tests.Fixtures
{
    internal class TestVariables
    {
        public string CityName { get; set; } = "London";
        public string InvalidCity { get; set; } = "InvalidCityName";
        public string Unit { get; set; } = "metric";
        public string DailyStep { get; set; } = "1d";
        public string HourlyStep { get; set; } = "1h";
    }
}
