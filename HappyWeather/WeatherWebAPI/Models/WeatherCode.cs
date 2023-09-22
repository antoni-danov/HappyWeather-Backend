namespace WeatherWebAPI.Models
{
    public enum WeatherCode
    {
        Unknown = 0,
        Clear, Sunny = 1000,
        Mostly_Clear = 1100,
        Partly_Cloudy = 1101,
        Mostly_Cloudy = 1102,
        Cloudy = 1001,
        Fog = 2000,
        Light_Fog = 2100,
        Drizzle = 4000,
        Rain = 4001,
        Light_Rain = 4200,
        Heavy_Rain = 4201,
        Snow = 5000,
        Flurries = 5001,
        Light_Snow = 5100,
        Heavy_Snow = 5101,
        Freezing_Drizzle = 6000,
        Freezing_Rain = 6001,
        Light_Freezing_Rain = 6200,
        Heavy_Freezing_Rain = 6201,
        Ice_Pellets = 7000,
        Heavy_Ice_Pellets = 7101,
        Light_Ice_Pellets = 7102,
        Thunderstorm = 8000
    }
}
