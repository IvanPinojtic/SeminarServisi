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
    [Route("api/Gost")]
    [Authorize]
    public class GostController : Controller
    {
        private readonly SeminarContext _context;

        public GostController(SeminarContext context)
        {
            _context = context;
        }

        // GET: api/Gost
        [HttpGet]
        public IEnumerable<Gost> GetGost()
        {
            return _context.Gost;
        }

        // GET: api/Gost/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gost = await _context.Gost.SingleOrDefaultAsync(m => m.Id == id);

            if (gost == null)
            {
                return NotFound();
            }

            return Ok(gost);
        }

        // PUT: api/Gost/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGost([FromRoute] int id, [FromBody] Gost gost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gost.Id)
            {
                return BadRequest();
            }

            _context.Entry(gost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GostExists(id))
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

        // POST: api/Gost
        [HttpPost]
        public async Task<IActionResult> PostGost([FromBody] Gost gost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Gost.Add(gost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGost", new { id = gost.Id }, gost);
        }

        // DELETE: api/Gost/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gost = await _context.Gost.SingleOrDefaultAsync(m => m.Id == id);
            if (gost == null)
            {
                return NotFound();
            }

            _context.Gost.Remove(gost);
            await _context.SaveChangesAsync();

            return Ok(gost);
        }

        private bool GostExists(int id)
        {
            return _context.Gost.Any(e => e.Id == id);
        }
    }
}