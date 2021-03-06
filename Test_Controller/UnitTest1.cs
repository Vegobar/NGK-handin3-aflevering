using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using NGK_handin3.Controllers;
using NGK_handin3.Data;
using NGK_handin3.HubbaBubba;
using NGK_handin3.Model;
using NSubstitute;
using SQLitePCL;
using Xunit;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

namespace Test_Controller
{
    public class UnitTest1
    {
        private DbContextOptions<ApplicationDbContext> _options;
        private SqliteConnection _connection;
        private WeatherObservationsController _uut;
        private IHubContext<WeatherHub> hub;

        public UnitTest1()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(_connection).Options;
            var context = new ApplicationDbContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
            dataseeder.seedData(context);
            
            hub = Substitute.For<IHubContext<WeatherHub>>();
            
            _uut = new WeatherObservationsController(context,hub);
        }

        [Fact]
        public async void Test_Single_Name()
        {
            //Arrange

            //Act
            var result = await _uut.getSingle();
            var okResult = result as OkObjectResult;
            var content = okResult.Value as WeatherObservation;
            //Assert
            Xunit.Assert.Equal("Germany",content.Name);
        }
        
        [Fact]
        public async void Test_get_Latest()
        {
            //Arrange
            DateTime TestTime = new DateTime(2020, 08, 02, 8, 25, 00);
            //Act
            var result = await _uut.getSingle();
            var okResult = result as OkObjectResult;
            var content = okResult.Value as WeatherObservation;
            //Assert
            Xunit.Assert.Equal(TestTime,content.Time);
        }


        [Theory]
        [InlineData("London",1)]
        [InlineData("Paris",2)]
        [InlineData("Denmark",3)]
        [InlineData("Germany",4)]
        public async void Test_byId(string name, int index)
        {
            //Arrange

            //Act
            var result = await _uut.GetWeatherObservation(index);
            var content = result.Value;
            //Assert
            Xunit.Assert.Equal(name, content.Name);
        }

        [Fact]
        public async void Test_byId_Fail()
        {
            var result = await _uut.GetWeatherObservation(20);
            Xunit.Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void Test_getThree()
        {
            //Arrange

            //Act
            var result = await _uut.GetWeather();
            var content = result.Value;
            List<WeatherObservation> testing = content as List<WeatherObservation>;
            
            //Assert
            Xunit.Assert.True(testing.Count == 3);
        }

        [Fact]
        public async void Test_bySingleDay()
        {
            //Arrange
            DateTime[] times = new DateTime[1] {new DateTime(2020, 08, 02, 8, 25, 00)};
            //Act
            var result = await _uut.GetWeatherForecast(times);
            var okResult = result.Result as OkObjectResult;
            var content = okResult.Value as List<WeatherObservation>;

            //Assert
            Xunit.Assert.Equal(1,content.Count);
        }

        
        [Fact]
        public async void Test_byDay()
        {
            //Arrange
            DateTime time1 = new DateTime(2020, 01, 01, 02, 00, 00);
            DateTime time2 = new DateTime(2020,06,03,02,00,00);
            DateTime[] times = new DateTime[2];
            times[0] = time1;
            times[1] = time2;
            //Act
            var result = await _uut.GetWeatherForecast(times);
            var okResult = result.Result as OkObjectResult;
            var content = okResult.Value as List<WeatherObservation>;
            //Assert
            Xunit.Assert.Equal(2, content.Count);
        }

        [Theory]
        [InlineData("Test",20.20,20.20,35,40,10,"Test")]
        [InlineData("Test2",400,400,20,30,21,"Test2")]
        [InlineData("Test3",52,52,35,21,33,"Test3")]
        
        public async void Test_post(string name, double lat, double lon, int temp, int hum, int air,string exp)
        {
            //Arrange
            var insert = new WeatherObservation()
            {
                Name = name,
                Latitude = lat,
                Longitude = lon,
                Temperature = temp,
                Humidity = hum,
                AirPressure = air
            };
            //Act
            await _uut.PostWeatherObservation(insert);
            var result = await _uut.getSingle();
            var okResult = result as OkObjectResult;
            var contest = okResult.Value as WeatherObservation;
            //Assert
            Xunit.Assert.Equal(insert,contest);

        }
        [Theory]
        [InlineData("Test",20.20,20.20,35,40,10,"Test")]
        [InlineData("Test2",400,400,20,30,21,"Test2")]
        [InlineData("Test3",52,52,35,21,33,"Test3")]
        public async void Test_post_value_lat(string name, double lat, double lon, int temp, int hum, int air,string exp)
        {
            //Arrange
            var insert = new WeatherObservation()
            {
                Name = name,
                Latitude = lat,
                Longitude = lon,
                Temperature = temp,
                Humidity = hum,
                AirPressure = air
            };
            //Act
            await _uut.PostWeatherObservation(insert);
            var result = await _uut.getSingle();
            var okResult = result as OkObjectResult;
            var contest = okResult.Value as WeatherObservation;
            //Assert
            Xunit.Assert.Equal(insert.Latitude,contest.Latitude);

        }
        [Theory]
        [InlineData("Test",20.20,20.20,35,40,10,"Test")]
        [InlineData("Test2",400,400,20,30,21,"Test2")]
        [InlineData("Test3",52,52,35,21,33,"Test3")]
        public async void Test_post_value_long(string name, double lat, double lon, int temp, int hum, int air,string exp)
        {
            //Arrange
            var insert = new WeatherObservation()
            {
                Name = name,
                Latitude = lat,
                Longitude = lon,
                Temperature = temp,
                Humidity = hum,
                AirPressure = air
            };
            //Act
            await _uut.PostWeatherObservation(insert);
            var result = await _uut.getSingle();
            var okResult = result as OkObjectResult;
            var contest = okResult.Value as WeatherObservation;
            //Assert
            Xunit.Assert.Equal(insert.Longitude,contest.Longitude);

        }

        [Theory]
        [InlineData("Test",20.20,20.20,35,40,10,"Test")]
        [InlineData("Test2",400,400,20,30,21,"Test2")]
        [InlineData("Test3",52,52,35,21,33,"Test3")]
        public async void Test_post_value_temp(string name, double lat, double lon, int temp, int hum, int air,string exp)
        {
            //Arrange
            var insert = new WeatherObservation()
            {
                Name = name,
                Latitude = lat,
                Longitude = lon,
                Temperature = temp,
                Humidity = hum,
                AirPressure = air
            };
            //Act
            await _uut.PostWeatherObservation(insert);
            var result = await _uut.getSingle();
            var okResult = result as OkObjectResult;
            var contest = okResult.Value as WeatherObservation;
            //Assert
            Xunit.Assert.Equal(insert.Temperature,contest.Temperature);

        }
        [Theory]
        [InlineData("Test",20.20,20.20,35,40,10,"Test")]
        [InlineData("Test2",400,400,20,30,21,"Test2")]
        [InlineData("Test3",52,52,35,21,33,"Test3")]
        public async void Test_post_value_hum(string name, double lat, double lon, int temp, int hum, int air,string exp)
        {
            //Arrange
            var insert = new WeatherObservation()
            {
                Name = name,
                Latitude = lat,
                Longitude = lon,
                Temperature = temp,
                Humidity = hum,
                AirPressure = air
            };
            //Act
            await _uut.PostWeatherObservation(insert);
            var result = await _uut.getSingle();
            var okResult = result as OkObjectResult;
            var contest = okResult.Value as WeatherObservation;
            //Assert
            Xunit.Assert.Equal(insert.Humidity,contest.Humidity);

        }

        [Theory]
        [InlineData("Test",20.20,20.20,35,40,10,"Test")]
        [InlineData("Test2",400,400,20,30,21,"Test2")]
        [InlineData("Test3",52,52,35,21,33,"Test3")]
        public async void Test_post_value_air(string name, double lat, double lon, int temp, int hum, int air,string exp)
        {
            //Arrange
            var insert = new WeatherObservation()
            {
                Name = name,
                Latitude = lat,
                Longitude = lon,
                Temperature = temp,
                Humidity = hum,
                AirPressure = air
            };
            //Act
            await _uut.PostWeatherObservation(insert);
            var result = await _uut.getSingle();
            var okResult = result as OkObjectResult;
            var contest = okResult.Value as WeatherObservation;
            //Assert
            Xunit.Assert.Equal(insert.AirPressure,contest.AirPressure);

        }

        [Fact]
        public void test_Authorize_test()
        {
            var ok = _uut.testing();
            Xunit.Assert.IsType<OkResult>(ok);
        }

        [Fact]
        public void test_index()
        {
            var result = _uut.index();
            Xunit.Assert.IsType<RedirectToPageResult>(result);
        }

    }

}

