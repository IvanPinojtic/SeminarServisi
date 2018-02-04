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
    [Route("api/Restoran")]
    [Authorize]
    public class RestoranController : Controller
    {
        private readonly SeminarContext _context;

        public RestoranController(SeminarContext context)
        {
            _context = context;
        }

        // GET: api/Restoran
        [HttpGet]
        public IEnumerable<Restoran> GetRestoran()
        {
            return _context.Restoran.Include(r => r.Adresa).Include(r => r.Gosti).Include(r => r.Menu);
        }

        // GET: api/Restoran/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestoran([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restoran = await _context.Restoran.SingleOrDefaultAsync(m => m.Id == id);

            if (restoran == null)
            {
                return NotFound();
            }

            return Ok(restoran);
        }

        // PUT: api/Restoran/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestoran([FromRoute] int id, [FromBody] Restoran restoran)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restoran.Id)
            {
                return BadRequest();
            }

            _context.Entry(restoran).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestoranExists(id))
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

        // POST: api/Restoran
        [HttpPost]
        public async Task<IActionResult> PostRestoran([FromBody] Restoran restoran)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Restoran.Add(restoran);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestoran", new { id = restoran.Id }, restoran);
        }

        // DELETE: api/Restoran/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestoran([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restoran = await _context.Restoran.SingleOrDefaultAsync(m => m.Id == id);
            if (restoran == null)
            {
                return NotFound();
            }

            _context.Restoran.Remove(restoran);
            await _context.SaveChangesAsync();

            return Ok(restoran);
        }

        private bool RestoranExists(int id)
        {
            return _context.Restoran.Any(e => e.Id == id);
        }
    }
}