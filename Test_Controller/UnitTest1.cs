using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NGK_handin3.Controllers;
using NGK_handin3.Data;
using NGK_handin3.HubbaBubba;
using NGK_handin3.Model;
using NSubstitute;
using SQLitePCL;
using Xunit;

namespace Test_Controller
{
    public class UnitTest1
    {
        private DbContextOptions<ApplicationDbContext> _options;
        private SqliteConnection _connection;

        [Fact]
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

            _connection = new SqliteConnection("DataSource=:memory");
            _connection.Open();
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(_connection).Options;
            var context = new ApplicationDbContext(_options);
            IHubContext<WeatherHub> hub = Substitute.For<IHubContext<WeatherHub>>();

            WeatherObservationsController weatherObservationsController = new WeatherObservationsController(context,hub);

            //Act
            await weatherObservationsController.PostWeatherObservation(weatherObservation);
            var result = await weatherObservationsController.GetWeatherObservation(1);

            //Assert
            var okResult = Xunit.Assert.IsType<WeatherObservation>(result);
            Xunit.Assert.Equal("London", okResult.Name);
          }
    }
}

