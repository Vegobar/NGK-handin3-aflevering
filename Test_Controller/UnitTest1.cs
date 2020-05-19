using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
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
        public async void Test_Observation()
        {
            //Arrange

            //Act
            var result = await _uut.getSingle();
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            var contest = okResult.Value;
            var obj = Xunit.Assert.IsType<WeatherObservation>(contest);
            //Assert
            Xunit.Assert.Equal("Germany",obj.Name);
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
        public async void Test_getThree()
        {
            //Arrange

            //Act
            var result = await _uut.GetWeather();
            var content = result.Value;
            List<WeatherObservation> testing = content.ToList();
            
            //Assert
            Xunit.Assert.True(testing.Count == 3);
        }
        
        [Fact]//Fail somehow??? 
        public async void Test_byDay()
        {
            //Arrange
            DateTime time1 = new DateTime(2020, 04, 01, 0, 0, 00);
            DateTime time2 = new DateTime(2020,06,03,0,00,00);
            DateTime[] times = new [] {time1, time2};
            //Act
            var result = await _uut.GetWeatherForecast(times);
            var content = result.Value;
            var count = content.Count;
            //Assert
            Xunit.Assert.Equal(count,2);
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
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            var contest = okResult.Value;
            var obj = Xunit.Assert.IsType<WeatherObservation>(contest);
            //Assert
            Xunit.Assert.Equal(name,exp);

        }

    }

}

