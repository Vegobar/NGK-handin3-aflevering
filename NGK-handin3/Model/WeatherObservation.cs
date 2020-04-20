using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGK_handin3.Model
{
    public class WeatherObservation
    {
        //Time
        public DateTime Time { get; set; }

        // Location
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Specifics about weather
        public decimal Temperatue { get; set; }
        public int Humidity { get; set; }
        public decimal AirPressure { get; set; }



    }
}
