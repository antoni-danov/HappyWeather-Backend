using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherWebAPI.Models.FiveDaysModel.DayModel;
using WeatherWebAPI.Models.HourlyForecastModel.HourModel;
using WeatherWebAPI.Models.RealTimeModel;
using WeatherWebAPI.Models.WeatherForecastModel;
using WeatherWebAPI.Tests.Fixtures;

namespace WeatherWebAPI.Tests.Controller
{
    public class WeatherControllerTests : IClassFixture<WeatherControllerFixture>
    {
        private readonly WeatherControllerFixture _fixture;
        private readonly TestVariables _variables;

        public WeatherControllerTests(WeatherControllerFixture fixture)
        {
            _fixture = fixture;
            _variables = new TestVariables();
        }

        [Fact]
        public async Task RealTimeForecast_ValidCityAndUnit_ReturnsOkResult()
        {
            //Arrange
            _fixture.MockService?.Setup(service => service.GetRealTimeForecast(_variables.cityName, _variables.unit))
                .ReturnsAsync(new WeatherResult());
            _fixture.Controller?.ModelState.Clear();

            //Act
            var result = await _fixture.Controller!.RealTimeForecast(_variables.cityName, _variables.unit);

            //Assert

            var actionResult = Assert.IsAssignableFrom<ActionResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var weatherResult = Assert.IsType<WeatherResult>(okResult.Value);
            Assert.NotNull(weatherResult);
        }

        [Fact]
        public async Task RealTimeForecast_InvalidCity_ReturnsBadRequestResult()
        {
            //Arrange
            _fixture.Controller?.ModelState.AddModelError("CityName", "The city name is required.");

            //Act
            var result = await _fixture.Controller!.RealTimeForecast(_variables.invalidCity, _variables.unit);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The city name is required.", badRequestResult.Value);

        }

        [Fact]

        public async Task DailyForecast_ValidCityAndUnit_ReturnsOKResult()
        {
            //Arrange
            _fixture.MockService?.Setup(service => service.GetDailyWeatherForecast(_variables.cityName, _variables.dailyStep, _variables.unit))
                .ReturnsAsync(new WeatherForecast<DayUnit>());
            _fixture.Controller?.ModelState.Clear();

            //Act
            var result = await _fixture.Controller!.DailyForecast(_variables.cityName, _variables.dailyStep, _variables.unit);

            //Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var dailyWeatherForecast = Assert.IsType<WeatherForecast<DayUnit>>(okResult.Value);
            Assert.NotNull(dailyWeatherForecast);
        }

        [Fact]
        public async Task DailyForecast_InvalidCity_ReturnsBadRequest()
        {
            //Arrange
            _fixture.Controller?.ModelState.AddModelError("CityName", "The city name is required.");
            //Act
            var result = await _fixture.Controller!.DailyForecast(_variables.invalidCity, _variables.dailyStep, _variables.unit);

            //Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The city name is required.", badRequest.Value);
        }

        [Fact]
        public async Task HourlyForecast_ValidCityAndUnit_ReturnsOKResult()
        {
            //Arrange
            _fixture.MockService?.Setup(service => service.GetHourlyWeatherForecast(_variables.cityName, _variables.hourlyStep, _variables.unit))
                .ReturnsAsync(new WeatherForecast<HourUnit>());
            _fixture.Controller?.ModelState.Clear();

            //Act
            var result = await _fixture.Controller!.HourlyForecast(_variables.cityName, _variables.hourlyStep, _variables.unit);

            //Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var hourlyWeatherForecast = Assert.IsType<WeatherForecast<HourUnit>>(okObjectResult.Value);
            Assert.NotNull(hourlyWeatherForecast);
        }

        [Fact]
        public async Task HourlyForecast_InvalidCity_ReturnsBadRequest()
        {
            //Arrange
            _fixture.Controller?.ModelState.AddModelError("CityName", "The city name is required.");

            //Act
            var result = await _fixture.Controller!.HourlyForecast(_variables.cityName, _variables.hourlyStep, _variables.unit);

            //Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The city name is required.", badRequest.Value);
        }
    }

}