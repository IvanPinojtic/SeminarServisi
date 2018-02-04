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
    [Route("api/Jelo")]
    [Authorize]
    public class JeloController : Controller
    {
        private readonly SeminarContext _context;

        public JeloController(SeminarContext context)
        {
            _context = context;
        }

        // GET: api/Jelo
        [HttpGet]
        public IEnumerable<Jelo> GetJelo()
        {
            return _context.Jelo;
        }

        // GET: api/Jelo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJelo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jelo = await _context.Jelo.SingleOrDefaultAsync(m => m.Id == id);

            if (jelo == null)
            {
                return NotFound();
            }

            return Ok(jelo);
        }

        // PUT: api/Jelo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJelo([FromRoute] int id, [FromBody] Jelo jelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jelo.Id)
            {
                return BadRequest();
            }

            _context.Entry(jelo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JeloExists(id))
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

        // POST: api/Jelo
        [HttpPost]
        public async Task<IActionResult> PostJelo([FromBody] Jelo jelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Jelo.Add(jelo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJelo", new { id = jelo.Id }, jelo);
        }

        // DELETE: api/Jelo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJelo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jelo = await _context.Jelo.SingleOrDefaultAsync(m => m.Id == id);
            if (jelo == null)
            {
                return NotFound();
            }

            _context.Jelo.Remove(jelo);
            await _context.SaveChangesAsync();

            return Ok(jelo);
        }

        private bool JeloExists(int id)
        {
            return _context.Jelo.Any(e => e.Id == id);
        }
    }
}