using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarMVCServis.DAL.Models;

namespace SeminarMVCServis.Controllers
{
    [Produces("application/json")]
    [Route("api/Guests")]
    [Authorize]
    public class GuestController : Controller
    {
        private readonly RestaurantsContext _context;

        public GuestController(RestaurantsContext context)
        {
            _context = context;
        }

        // GET: api/Guest
        [HttpGet]
        public IEnumerable<Guest> GetGuest()
        {
            return _context.Guests;
        }

        // GET: api/Guest/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Guest = await _context.Guests.SingleOrDefaultAsync(m => m.Id == id);

            if (Guest == null)
            {
                return NotFound();
            }

            return Ok(Guest);
        }

        // PUT: api/Guest/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuest([FromRoute] int id, [FromBody] Guest Guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Guest.Id)
            {
                return BadRequest();
            }

            _context.Entry(Guest).State = EntityState.Modified;

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

        // POST: api/Guest
        [HttpPost]
        public async Task<IActionResult> PostGuest([FromBody] Guest Guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Guests.Add(Guest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGuest", new { id = Guest.Id }, Guest);
        }

        // DELETE: api/Guest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Guest = await _context.Guests.SingleOrDefaultAsync(m => m.Id == id);
            if (Guest == null)
            {
                return NotFound();
            }

            _context.Guests.Remove(Guest);
            await _context.SaveChangesAsync();

            return Ok(Guest);
        }

        private bool GuestExists(int id)
        {
            return _context.Guests.Any(e => e.Id == id);
        }
    }
}