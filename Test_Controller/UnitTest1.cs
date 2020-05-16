using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NGK_handin3.Controllers;
using NGK_handin3.Data;
using NGK_handin3.Model;
using NSubstitute;
using Xunit;

namespace Test_Controller
{
    [TestClass]
    public class UnitTest1
    {
        private ApplicationDbContext dbContext;

        [TestMethod]
        public async void CheckId()
        {
            //Arrange
            WeatherObservation weatherObservation = new WeatherObservation();
            weatherObservation.Name = "London";
            weatherObservation.Latitude = 135.5;
            weatherObservation.Longitude = 90.50;
            weatherObservation.Temperature = 20;
            weatherObservation.Humidity = 80;
            weatherObservation.AirPressure = 27;

            dbContext = Substitute.For<ApplicationDbContext>();
            WeatherObservationsController weatherObservationsController = new WeatherObservationsController(dbContext);

            //Act
            await weatherObservationsController.PostWeatherObservation(weatherObservation);
            var result = await weatherObservationsController.GetWeatherObservation(1);

            //Assert
            var okResult = Xunit.Assert.IsType<WeatherObservation>(result);
            Xunit.Assert.Equal("London", okResult.Name);
          }
    }
}

