using System;
using System.Collections.Generic;
using System.Linq;
using NGK_handin3.Model;
using Microsoft.EntityFrameworkCore;

namespace NGK_handin3.Data
{
    public class Seeder
    {
        public static void SeedData(ApplicationDbContext db)
        {
            seed(db);
        }

        public static void seed(ApplicationDbContext db)
        {
            if (!db.Weather.Any())
            {
                List<WeatherObservation> obs = new List<WeatherObservation>();
                var o = new WeatherObservation()
                {
                    AirPressure = 40.0m,
                    Humidity = 20,
                    Latitude = 100.21,
                    Longitude = 200.21,
                    Name = "Station 1",
                    Temperature = 40,
                    Time = new DateTime(2020, 04, 02, 8, 25,00),
                };
                obs.Add(o);
                o = new WeatherObservation()
                {
                    AirPressure = 45.0m,
                    Humidity = 22,
                    Latitude = 100.21,
                    Longitude = 200.21,
                    Name = "Station 1",
                    Temperature = 39,
                    Time = new DateTime(2020, 04, 03, 15, 20,00),
                };
                obs.Add(o);
                o = new WeatherObservation()
                {
                    AirPressure = 20.0m,
                    Humidity = 10,
                    Latitude = 201.21,
                    Longitude = 120.21,
                    Name = "Station 2",
                    Temperature = 50,
                    Time = new DateTime(2020, 06, 21, 12, 03,00),
                };
                obs.Add(o);
                o = new WeatherObservation()
                {
                    AirPressure = 45.0m,
                    Humidity = 25,
                    Latitude = 100.21,
                    Longitude = 200.21,
                    Name = "Station 1",
                    Temperature = 20,
                    Time = new DateTime(2021, 01, 05, 5, 25,00),
                };
                obs.Add(o);
                o = new WeatherObservation()
                {
                    AirPressure = 40.0m,
                    Humidity = 20,
                    Latitude = 100.21,
                    Longitude = 200.21,
                    Name = "Station 1",
                    Temperature = 40,
                    Time = new DateTime(2021, 04, 02, 8, 25,00),
                };
                obs.Add(o);
                o = new WeatherObservation()
                {
                    AirPressure = 40.0m,
                    Humidity = 10,
                    Latitude = 100.21,
                    Longitude = 200.21,
                    Name = "Station 1",
                    Temperature = 30,
                    Time = new DateTime(2021, 09, 04, 7, 12,00),
                };
                obs.Add(o);
                o = new WeatherObservation()
                {
                    AirPressure = 40.0m,
                    Humidity = 20,
                    Latitude = 100.21,
                    Longitude = 200.21,
                    Name = "Station 1",
                    Temperature = 40,
                    Time = new DateTime(2022, 06, 05, 14, 25,00),
                };
                obs.Add(o);
                o = new WeatherObservation()
                {
                    AirPressure = 40.0m,
                    Humidity = 20,
                    Latitude = 100.21,
                    Longitude = 200.21,
                    Name = "Station 1",
                    Temperature = 40,
                    Time = new DateTime(2020, 08, 03, 18, 10,00),
                };
                obs.Add(o);
                o = new WeatherObservation()
                {
                    AirPressure = 40.0m,
                    Humidity = 20,
                    Latitude = 100.21,
                    Longitude = 200.21,
                    Name = "Station 1",
                    Temperature = 40,
                    Time = new DateTime(2020, 10, 12, 16, 10,00),
                };
                obs.Add(o);
                o = new WeatherObservation()
                {
                    AirPressure = 40.0m,
                    Humidity = 20,
                    Latitude = 100.21,
                    Longitude = 200.21,
                    Name = "Station 1",
                    Temperature = 40,
                    Time = new DateTime(2021, 12, 24, 20, 25,00),
                };
                obs.Add(o);
                o = new WeatherObservation()
                {
                    AirPressure = 40.0m,
                    Humidity = 20,
                    Latitude = 100.21,
                    Longitude = 200.21,
                    Name = "Station 1",
                    Temperature = 40,
                    Time = new DateTime(2020, 02, 15, 11, 25,00),
                };
                obs.Add(o);
                o = new WeatherObservation()
                {
                    AirPressure = 40.0m,
                    Humidity = 20,
                    Latitude = 100.21,
                    Longitude = 200.21,
                    Name = "Station 1",
                    Temperature = 40,
                    Time = new DateTime(2020, 07, 25, 22, 10,00),
                };
                obs.Add(o);
                o = new WeatherObservation()
                {
                    AirPressure = 40.0m,
                    Humidity = 20,
                    Latitude = 100.21,
                    Longitude = 200.21,
                    Name = "Station 1",
                    Temperature = 40,
                    Time = new DateTime(2020, 03, 24, 23, 25,00),
                };
                obs.Add(o);
                db.AddRange(obs);
                var wait = db.SaveChangesAsync();

            }
        }
    }
}