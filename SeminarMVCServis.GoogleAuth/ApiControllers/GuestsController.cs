using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarMVCServis.DAL.Models;

namespace SeminarMVCServis.GoogleAuth.ApiControllers
{
    [Produces("application/json")]
    [Route("api/Guests")]
    [ApiVersion("1.0")]
    [Authorize]
    public class GuestsController : Controller
    {
        private readonly RestaurantsContext _context;

        public GuestsController(RestaurantsContext context)
        {
            _context = context;
        }

        // GET: api/Guests
        [HttpGet]
        public IEnumerable<Guest> GetGuests()
        {
            return _context.Guests;
        }

        // GET: api/Guests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guest = await _context.Guests.SingleOrDefaultAsync(m => m.Id == id);

            if (guest == null)
            {
                return NotFound();
            }

            return Ok(guest);
        }

        // PUT: api/Guests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuest([FromRoute] int id, [FromBody] Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guest.Id)
            {
                return BadRequest();
            }

            _context.Entry(guest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(id))
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

        // POST: api/Guests
        [HttpPost]
        public async Task<IActionResult> PostGuest([FromBody] Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGuest", new { id = guest.Id }, guest);
        }

        // DELETE: api/Guests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guest = await _context.Guests.SingleOrDefaultAsync(m => m.Id == id);
            if (guest == null)
            {
                return NotFound();
            }

            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();

            return Ok(guest);
        }

        private bool GuestExists(int id)
        {
            return _context.Guests.Any(e => e.Id == id);
        }
    }
}