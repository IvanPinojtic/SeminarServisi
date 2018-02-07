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
    [Route("api/Meals")]
    [Authorize]
    public class MealsController : Controller
    {
        private readonly RestaurantsContext _context;

        public MealsController(RestaurantsContext context)
        {
            _context = context;
        }

        // GET: api/Meals
        [HttpGet]
        public IEnumerable<Meal> GetMeal()
        {
            return _context.Meals;
        }

        // GET: api/Meals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Meal = await _context.Meals.SingleOrDefaultAsync(m => m.Id == id);

            if (Meal == null)
            {
                return NotFound();
            }

            return Ok(Meal);
        }

        // PUT: api/Meals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal([FromRoute] int id, [FromBody] Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meal.Id)
            {
                return BadRequest();
            }

            _context.Entry(meal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
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

        // POST: api/Meals
        [HttpPost]
        public async Task<IActionResult> PostMeal([FromBody] Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeal", new { id = meal.Id }, meal);
        }

        // DELETE: api/Meals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meal = await _context.Meals.SingleOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();

            return Ok(meal);
        }

        private bool MealExists(int id)
        {
            return _context.Meals.Any(e => e.Id == id);
        }
    }
}