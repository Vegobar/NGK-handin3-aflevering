using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NGK_handin3.Data;
using NGK_handin3.HubbaBubba;
using NGK_handin3.Model;

namespace NGK_handin3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherObservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<WeatherHub> _myHub;

        public WeatherObservationsController(ApplicationDbContext context, IHubContext<WeatherHub> hub)
        {
            _context = context;
            _myHub = hub;
        }

        [HttpGet]
        public IActionResult index()
        {
            return RedirectToPage(nameof(index));
        }

        [Authorize]
        [HttpGet("test")]
        public ActionResult testing()
        {
            return Ok();
        }


        [HttpGet("single")]
        public async Task<IActionResult> getSingle()
        {
            var latest = await (from p in _context.Weather
                orderby p.WeatherObservationId descending
                select p).FirstOrDefaultAsync();

            return Ok(latest);

        }
        // GET: api/WeatherObservations/GetWeather
        [HttpGet("GetWeather")]
        public async Task<ActionResult<IEnumerable<WeatherObservation>>> GetWeather()
        {

            List<WeatherObservation> three_returned = new List<WeatherObservation>();

            var weather = await (from p in _context.Weather
                select p).ToListAsync();

            if (weather.Count() >= 4)
            {
                for (int i = weather.Count() - 3; i < weather.Count(); i++)
                {
                    three_returned.Add(weather.ElementAt(i));
                }

                three_returned.Reverse();
                return three_returned;
            }

            return weather;
        }
        
        
        [HttpGet("GetWeatherForecasts")]
        public async Task<ActionResult<List<WeatherObservation>>> GetWeatherForecast([FromBody]DateTime[] times)
        {

            var myWeather = await (from p in _context.Weather
                where (p.Time >= times[0] && p.Time <= times[1])
                select p).ToListAsync();

         return Ok(myWeather);
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
        [Authorize]
        public async Task<ActionResult<WeatherObservation>> PostWeatherObservation(WeatherObservation weatherObservation)
        {
            _context.Weather.Add(weatherObservation);
            await _context.SaveChangesAsync();
            _myHub.Clients.All.SendAsync("WeatherUpdate", null);
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
