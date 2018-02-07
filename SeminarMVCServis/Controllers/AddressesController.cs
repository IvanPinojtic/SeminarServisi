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
    [Route("api/Addresses")]
    [Authorize]
    public class AddressesController : Controller
    {
        private readonly RestaurantsContext _context;

        public AddressesController(RestaurantsContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public IEnumerable<Address> GetAddress()
        {
            return _context.Addresses;
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Address = await _context.Addresses.SingleOrDefaultAsync(m => m.Id == id);

            if (Address == null)
            {
                return NotFound();
            }

            return Ok(Address);
        }

        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress([FromRoute] int id, [FromBody] Address Address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Address.Id)
            {
                return BadRequest();
            }

            _context.Entry(Address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        [HttpPost]
        public async Task<IActionResult> PostAddress([FromBody] Address Address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Addresses.Add(Address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = Address.Id }, Address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Address = await _context.Addresses.SingleOrDefaultAsync(m => m.Id == id);
            if (Address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(Address);
            await _context.SaveChangesAsync();

            return Ok(Address);
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }
    }
}