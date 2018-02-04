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
    [Route("api/GostJelo")]
    [Authorize]
    public class GostJeloController : Controller
    {
        private readonly SeminarContext _context;

        public GostJeloController(SeminarContext context)
        {
            _context = context;
        }

        // GET: api/GostJelo
        [HttpGet]
        public IEnumerable<GostJelo> GetGostJelo()
        {
            return _context.GostJelo.Include(g => g.Gost).Include(j => j.Jelo);
        }

        // GET: api/GostJelo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGostJelo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gostJelo = await _context.GostJelo.SingleOrDefaultAsync(m => m.Id == id);

            if (gostJelo == null)
            {
                return NotFound();
            }

            return Ok(gostJelo);
        }

        // PUT: api/GostJelo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGostJelo([FromRoute] int id, [FromBody] GostJelo gostJelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gostJelo.Id)
            {
                return BadRequest();
            }

            _context.Entry(gostJelo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GostJeloExists(id))
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

        // POST: api/GostJelo
        [HttpPost]
        public async Task<IActionResult> PostGostJelo([FromBody] GostJelo gostJelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GostJelo.Add(gostJelo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGostJelo", new { id = gostJelo.Id }, gostJelo);
        }

        // DELETE: api/GostJelo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGostJelo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gostJelo = await _context.GostJelo.SingleOrDefaultAsync(m => m.Id == id);
            if (gostJelo == null)
            {
                return NotFound();
            }

            _context.GostJelo.Remove(gostJelo);
            await _context.SaveChangesAsync();

            return Ok(gostJelo);
        }

        private bool GostJeloExists(int id)
        {
            return _context.GostJelo.Any(e => e.Id == id);
        }
    }
}