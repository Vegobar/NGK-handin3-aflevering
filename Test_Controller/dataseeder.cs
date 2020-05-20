using System;
using System.Collections.Generic;
using NGK_handin3.Data;
using NGK_handin3.Model;

namespace Test_Controller
{
    public class dataseeder
    {
        public static void seedData(ApplicationDbContext db)
        {
            List<WeatherObservation> list = new List<WeatherObservation>();

            var obs = new WeatherObservation()
            {
                Name = "London",
                Latitude = 135.5,
                Longitude = 90.50,
                Temperature = 20,
                Humidity = 80,
                AirPressure = 27,
                Time = new DateTime(2020, 04, 02, 8, 25,00),
            };
            list.Add(obs);
            obs = new WeatherObservation()
            {
                Name = "Paris",
                Latitude = 615.5,
                Longitude = 100.50,
                Temperature = 20,
                Humidity = 60,
                AirPressure = 22,
                Time = new DateTime(2020, 05, 02, 8, 25,00),
            };
            list.Add(obs);
            obs = new WeatherObservation()
            {
                Name = "Denmark",
                Latitude = 255.5,
                Longitude = 200.50,
                Temperature = 35,
                Humidity = 40,
                AirPressure = 35,
                Time = new DateTime(2021, 02, 02, 8, 25,00)
            };
            list.Add(obs);
            obs = new WeatherObservation()
            {
                Name = "Germany",
                Latitude = 150.5,
                Longitude = 80.50,
                Temperature = 25,
                Humidity = 20,
                AirPressure = 15,
                Time = new DateTime(2020, 08, 02, 8, 25,00)
            };
            list.Add(obs);
            db.AddRange(list);
            db.SaveChangesAsync();
        }
        
    }
}