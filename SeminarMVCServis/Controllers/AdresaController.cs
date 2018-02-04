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
    [Route("api/Adresa")]
    [Authorize]
    public class AdresaController : Controller
    {
        private readonly SeminarContext _context;

        public AdresaController(SeminarContext context)
        {
            _context = context;
        }

        // GET: api/Adresa
        [HttpGet]
        public IEnumerable<Adresa> GetAdresa()
        {
            return _context.Adresa;
        }

        // GET: api/Adresa/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdresa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adresa = await _context.Adresa.SingleOrDefaultAsync(m => m.Id == id);

            if (adresa == null)
            {
                return NotFound();
            }

            return Ok(adresa);
        }

        // PUT: api/Adresa/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdresa([FromRoute] int id, [FromBody] Adresa adresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adresa.Id)
            {
                return BadRequest();
            }

            _context.Entry(adresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdresaExists(id))
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

        // POST: api/Adresa
        [HttpPost]
        public async Task<IActionResult> PostAdresa([FromBody] Adresa adresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Adresa.Add(adresa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdresa", new { id = adresa.Id }, adresa);
        }

        // DELETE: api/Adresa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdresa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adresa = await _context.Adresa.SingleOrDefaultAsync(m => m.Id == id);
            if (adresa == null)
            {
                return NotFound();
            }

            _context.Adresa.Remove(adresa);
            await _context.SaveChangesAsync();

            return Ok(adresa);
        }

        private bool AdresaExists(int id)
        {
            return _context.Adresa.Any(e => e.Id == id);
        }
    }
}