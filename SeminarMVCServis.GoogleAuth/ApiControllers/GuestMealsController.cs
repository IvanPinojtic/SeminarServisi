using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarMVCServis.DAL.Models;

namespace SeminarMVCServis.GoogleAuth.Controllers
{
    [Produces("application/json")]
    [Route("api/GuestMeals")]
    [ApiVersion("1.0")]
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

            var guestMeal = await _context.GuestMeals.Include(m => m.Meal).Include(g => g.Guest).ThenInclude(g => g.Restaurant).SingleOrDefaultAsync(g => g.GuestId == id);

            if (guestMeal == null)
            {
                return NotFound();
            }

            return Ok(guestMeal);
        }

        [HttpGet("GetGuestMealByGuestId/{n:int?}")]
        public IActionResult GetGuestMealsByGuestId(int n)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guestMeals = _context.GuestMeals.Include(m => m.Meal).Include(g => g.Guest).ThenInclude(g => g.Restaurant).Where(g => g.GuestId == n);

            if (guestMeals == null)
            {
                return NotFound();
            }

            return Ok(guestMeals);
        }

        // PUT: api/GuestMeal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuestMeal([FromRoute] int id, [FromBody] GuestMeal guestMeal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guestMeal.Id)
            {
                return BadRequest();
            }

            _context.Entry(guestMeal).State = EntityState.Modified;

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
        public async Task<IActionResult> PostGuestMeal([FromBody] GuestMeal guestMeal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GuestMeals.Add(guestMeal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGuestMeal", new { id = guestMeal.Id }, guestMeal);
        }

        // DELETE: api/GuestMeal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuestMeal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guestMeal = await _context.GuestMeals.SingleOrDefaultAsync(m => m.Id == id);
            if (guestMeal == null)
            {
                return NotFound();
            }

            _context.GuestMeals.Remove(guestMeal);
            await _context.SaveChangesAsync();

            return Ok(guestMeal);
        }

        private bool GuestMealExists(int id)
        {
            return _context.GuestMeals.Any(e => e.Id == id);
        }
    }
}