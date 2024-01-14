using Microsoft.Extensions.Logging;
using Moq;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Services.WeatherService;

namespace WeatherWebAPI.Tests.Fixtures
{
    public class WeatherControllerFixture : IDisposable
    {
        public WeatherControllerFixture()
        {
            MockService = new Mock<IWeatherService>();
            Controller = new WeatherController(Logger!, MockService.Object);
        }
        public WeatherController? Controller { get; private set; }
        public Mock<IWeatherService>? MockService { get; private set; }
        public Logger<WeatherController>? Logger { get; private set; }
        public void Dispose()
        {
            Controller = null;
            MockService = null;
            Logger = null;
        }
    }
}
