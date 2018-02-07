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
    [Route("api/GuestMeals")]
    [Authorize]
    public class GuestMealsController : Controller
    {
        private readonly RestaurantsContext _context;

        public GuestMealsController(RestaurantsContext context)
        {
            _context = context;
        }

        // GET: api/GuestMeal
        [HttpGet]
        public IEnumerable<GuestMeal> GetGuestMeal()
        {
            return _context.GuestMeals.Include(g => g.Guest).Include(j => j.Meal);
        }

        // GET: api/GuestMeal/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuestMeal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var GuestMeal = await _context.GuestMeals.SingleOrDefaultAsync(m => m.Id == id);

            if (GuestMeal == null)
            {
                return NotFound();
            }

            return Ok(GuestMeal);
        }

        // PUT: api/GuestMeal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuestMeal([FromRoute] int id, [FromBody] GuestMeal GuestMeal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != GuestMeal.Id)
            {
                return BadRequest();
            }

            _context.Entry(GuestMeal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestMealExists(id))
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

        // POST: api/GuestMeal
        [HttpPost]
        public async Task<IActionResult> PostGuestMeal([FromBody] GuestMeal GuestMeal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GuestMeals.Add(GuestMeal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGuestMeal", new { id = GuestMeal.Id }, GuestMeal);
        }

        // DELETE: api/GuestMeal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuestMeal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var GuestMeal = await _context.GuestMeals.SingleOrDefaultAsync(m => m.Id == id);
            if (GuestMeal == null)
            {
                return NotFound();
            }

            _context.GuestMeals.Remove(GuestMeal);
            await _context.SaveChangesAsync();

            return Ok(GuestMeal);
        }

        private bool GuestMealExists(int id)
        {
            return _context.GuestMeals.Any(e => e.Id == id);
        }
    }
}