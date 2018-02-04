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
    [Route("api/SastojakJelo")]
    [Authorize]
    public class SastojakJeloController : Controller
    {
        private readonly SeminarContext _context;

        public SastojakJeloController(SeminarContext context)
        {
            _context = context;
        }

        // GET: api/SastojakJelo
        [HttpGet]
        public IEnumerable<SastojakJelo> GetSastojakJelo()
        {
            return _context.SastojakJelo.Include(s=>s.Sastojak).Include(j=>j.Jelo);
        }

        // GET: api/SastojakJelo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSastojakJelo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sastojakJelo = await _context.SastojakJelo.SingleOrDefaultAsync(m => m.Id == id);

            if (sastojakJelo == null)
            {
                return NotFound();
            }

            return Ok(sastojakJelo);
        }

        // PUT: api/SastojakJelo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSastojakJelo([FromRoute] int id, [FromBody] SastojakJelo sastojakJelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sastojakJelo.Id)
            {
                return BadRequest();
            }

            _context.Entry(sastojakJelo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SastojakJeloExists(id))
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

        // POST: api/SastojakJelo
        [HttpPost]
        public async Task<IActionResult> PostSastojakJelo([FromBody] SastojakJelo sastojakJelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SastojakJelo.Add(sastojakJelo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSastojakJelo", new { id = sastojakJelo.Id }, sastojakJelo);
        }

        // DELETE: api/SastojakJelo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSastojakJelo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sastojakJelo = await _context.SastojakJelo.SingleOrDefaultAsync(m => m.Id == id);
            if (sastojakJelo == null)
            {
                return NotFound();
            }

            _context.SastojakJelo.Remove(sastojakJelo);
            await _context.SaveChangesAsync();

            return Ok(sastojakJelo);
        }

        private bool SastojakJeloExists(int id)
        {
            return _context.SastojakJelo.Any(e => e.Id == id);
        }
    }
}