using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NGK_handin3.Model
{
    public class WeatherObservation
    {
        public int WeatherObservationId { get; set; }
        //Time
        public Time Time { get; set; } = new Time();

        // Location
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Specifics about weather
        public decimal Temperature { get; set; }
        public int Humidity { get; set; }
        public decimal AirPressure { get; set; }



    }

    public class Time
    {
        [Key]
        public int id { get; set; }

        public int year { get; set; } = DateTime.Now.Year;
        public int day { get; set; } = DateTime.Now.Day;
        public int month { get; set; } = DateTime.Now.Month;
        public int hour { get; set; } = DateTime.Now.Hour;
        public int minutes { get; set; } = DateTime.Now.Minute;
    }
}
