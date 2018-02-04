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
    [Route("api/Sastojak")]
    [Authorize]
    public class SastojakController : Controller
    {
        private readonly SeminarContext _context;

        public SastojakController(SeminarContext context)
        {
            _context = context;
        }

        // GET: api/Sastojak
        [HttpGet]
        public IEnumerable<Sastojak> GetSastojak()
        {
            return _context.Sastojak;
        }

        // GET: api/Sastojak/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSastojak([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sastojak = await _context.Sastojak.SingleOrDefaultAsync(m => m.Id == id);

            if (sastojak == null)
            {
                return NotFound();
            }

            return Ok(sastojak);
        }

        // PUT: api/Sastojak/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSastojak([FromRoute] int id, [FromBody] Sastojak sastojak)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sastojak.Id)
            {
                return BadRequest();
            }

            _context.Entry(sastojak).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SastojakExists(id))
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

        // POST: api/Sastojak
        [HttpPost]
        public async Task<IActionResult> PostSastojak([FromBody] Sastojak sastojak)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Sastojak.Add(sastojak);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSastojak", new { id = sastojak.Id }, sastojak);
        }

        // DELETE: api/Sastojak/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSastojak([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sastojak = await _context.Sastojak.SingleOrDefaultAsync(m => m.Id == id);
            if (sastojak == null)
            {
                return NotFound();
            }

            _context.Sastojak.Remove(sastojak);
            await _context.SaveChangesAsync();

            return Ok(sastojak);
        }

        private bool SastojakExists(int id)
        {
            return _context.Sastojak.Any(e => e.Id == id);
        }
    }
}