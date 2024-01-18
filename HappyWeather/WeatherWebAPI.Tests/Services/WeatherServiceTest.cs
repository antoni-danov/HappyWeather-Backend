using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using WeatherWebAPI.Models;
using WeatherWebAPI.Models.RealTimeModel;
using WeatherWebAPI.Services.WeatherService;
using WeatherWebAPI.Tests.Fixtures;

namespace WeatherWebAPI.Tests.Services
{
    public class WeatherServiceTest: IClassFixture<WeatherServiceFixture>
    {
        private readonly TestVariables? _variables = new TestVariables();
        private readonly WeatherServiceFixture _fixture;

        public WeatherServiceTest(WeatherServiceFixture fixture)
        {
            _fixture = fixture;
            Environment.SetEnvironmentVariable("TOMORROW_BASE_URL", "https://api.tomorrow.io/v4/");
            Environment.SetEnvironmentVariable("TOMORROW_API_KEY", "mocked_api_key");
        }

        [Fact]
        public async Task GetRealTimeForecast_HttpContext_Return_Success()
        {
            //Arrange
            var baseURL = $"{Environment.GetEnvironmentVariable("TOMORROW_BASE_URL")}weather/realtime?location={_variables?.CityName}&language=en-US&units={_variables?.Unit}&apikey={Environment.GetEnvironmentVariable("TOMORROW_API_KEY")}";
            DateTime expectedExpiration = DateTime.UtcNow.AddMinutes(5);

            _fixture.Client.BaseAddress = new Uri(baseURL);
            
            var weatherResult = new WeatherResult {Data = new WeatherData(), Location = new WeatherLocation()};
            string jsonContent = JsonConvert.SerializeObject(weatherResult);

            var setupApiRequest = _fixture.MockHandler.Protected().Setup<Task<HttpResponseMessage>>(
         "SendAsync",
         ItExpr.IsAny<HttpRequestMessage>(),
         ItExpr.IsAny<CancellationToken>()
         );
            var apiMockedResponse =
                setupApiRequest.ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
                });

            //TODO
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            _fixture.Cache.Setup(m => m.Set(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<MemoryCacheEntryOptions>()))
                .Callback<object, object, MemoryCacheEntryOptions>((key, value, options) =>
                {
                    var entryOptions = new MemoryCacheEntryOptions();
                    entryOptions.AbsoluteExpiration = expectedExpiration;
                });


            var service = new WeatherService(_fixture.Client, _fixture.Cache.Object);
            //Act
            var result = await service.GetRealTimeForecast(_variables!.CityName, _variables!.Unit);
            //Assert
            Assert.Equal(weatherResult, result);
            Assert.NotNull(result);
        }
    }
}
