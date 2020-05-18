using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NGK_handin3;

namespace UnitTest_Controller
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

            dbContext = Substitute.For<ApplicationDbCoAntext>();
            WeatherObservationsController weatherObservationsController = new WeatherObservationsController(dbContext);

            //Act
            await weatherObservationsController.PostWeatherObservation(weatherObservation);
            var result = await weatherObservationsController.GetWeatherObservation(1);

            //Assert
            var okResult = Assert.IsType<ViewRes>
            }
    }
}
