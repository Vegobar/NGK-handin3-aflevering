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
        public DateTime Time { get; set; } = DateTime.Now;

        // Location
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Specifics about weather
        public decimal Temperature { get; set; }
        public int Humidity { get; set; }
        public decimal AirPressure { get; set; }



    }
}
