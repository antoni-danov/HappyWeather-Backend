using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.Protected;
using WeatherWebAPI.Models.RealTimeModel;

namespace WeatherWebAPI.Tests.Fixtures
{
    public class WeatherServiceFixture : IDisposable
    {
        private readonly TestVariables _variables = new TestVariables();
        public WeatherServiceFixture()
        {
            MockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage());

            Client = new HttpClient(MockHandler.Object);

        }

        public Mock<HttpMessageHandler> MockHandler { get; set; } = new Mock<HttpMessageHandler>();
        public HttpClient Client { get; set; }
        public Mock<IMemoryCache> Cache { get; set; } = new Mock<IMemoryCache>();

        public void Dispose()
        {
            Cache.Object.Dispose();
            Client.Dispose();
        }
    }
}
