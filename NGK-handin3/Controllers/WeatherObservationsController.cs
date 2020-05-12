using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGK_handin3.Data;
using NGK_handin3.Model;

namespace NGK_handin3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherObservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WeatherObservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult index()
        {
            return RedirectToPage(nameof(index));
        }

        // GET: api/WeatherObservations/GetWeather
        [HttpGet("GetWeather")]
        public async Task<ActionResult<IEnumerable<WeatherObservation>>> GetWeather()
        {
            var weather = await _context.Weather.ToListAsync();
            List<WeatherObservation> three_returned = new List<WeatherObservation>();

            if (weather.Count() >= 4)
            {
                for (int i = weather.Count() - 3; i < weather.Count(); i++)
                {
                    three_returned.Add(weather.ElementAt(i));
                }
                return three_returned;
            }

            return weather;
        }

        //GET: api/WeatherObservations/GetWeatherForecast
        [HttpGet("GetWeatherForecast")]
        public async Task<ActionResult<IEnumerable<WeatherObservation>>>GetWeatherForecast(DateTime start, DateTime stop)
        {
            var weather = await _context.Weather
                .Where(p => Int32.Parse(p.Time.month) >= start.Month && Int32.Parse(p.Time.day) >= start.Day && Int32.Parse(p.Time.hour) >= start.Hour
                && Int32.Parse(p.Time.month) <= stop.Month && Int32.Parse(p.Time.day)<= stop.Month && Int32.Parse(p.Time.hour)<=stop.Hour).ToListAsync();

            return weather;
        }
        

        // GET: api/WeatherObservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherObservation>> GetWeatherObservation(int id)
        {
            var weatherObservation = await _context.Weather.FindAsync(id);

            if (weatherObservation == null)
            {
                return NotFound();
            }

            return weatherObservation;
        }

        // PUT: api/WeatherObservations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherObservation(int id, WeatherObservation weatherObservation)
        {
            if (id != weatherObservation.WeatherObservationId)
            {
                return BadRequest();
            }

            _context.Entry(weatherObservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherObservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WeatherObservations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<WeatherObservation>> PostWeatherObservation(WeatherObservation weatherObservation)
        {
            _context.Weather.Add(weatherObservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeatherObservation", new { id = weatherObservation.WeatherObservationId }, weatherObservation);
        }

        // DELETE: api/WeatherObservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WeatherObservation>> DeleteWeatherObservation(int id)
        {
            var weatherObservation = await _context.Weather.FindAsync(id);
            if (weatherObservation == null)
            {
                return NotFound();
            }

            _context.Weather.Remove(weatherObservation);
            await _context.SaveChangesAsync();

            return weatherObservation;
        }

        private bool WeatherObservationExists(int id)
        {
            return _context.Weather.Any(e => e.WeatherObservationId == id);
        }
    }
}
