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
    [Route("api/IngredientMeals")]
    [ApiVersion("1.0")]
    [Authorize]
    public class IngredientMealsController : Controller
    {
        private readonly RestaurantsContext _context;

        public IngredientMealsController(RestaurantsContext context)
        {
            _context = context;
        }

        // GET: api/IngredientMeals
        [HttpGet]
        public IEnumerable<IngredientMeal> GetIngredientMeal()
        {
            return _context.IngredientMeals.Include(s => s.Ingredient).Include(j => j.Meal);
        }

        // GET: api/IngredientMeals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientMeal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingredientMeal = await _context.IngredientMeals.Include(s => s.Ingredient).Include(m => m.Meal).ThenInclude(m => m.Restaurant).SingleOrDefaultAsync(m => m.Id == id);

            if (ingredientMeal == null)
            {
                return NotFound();
            }

            return Ok(ingredientMeal);
        }

        [HttpGet("GetIngredientMealByMealId/{n:int?}")]
        public IActionResult GetIngredientMealsByMealId(int n)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingredientMeals = _context.IngredientMeals.Include(s => s.Ingredient).Include(m => m.Meal).ThenInclude(m => m.Restaurant).Where(m => m.Meal.Id == n);

            if (ingredientMeals == null)
            {
                return NotFound();
            }

            return Ok(ingredientMeals);
        }

        [HttpGet("GetIngredientMealsByIngredientId/{n:int?}")]
        public IActionResult GetIngredientMealsByIngredientId(int n)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingredientMeals = _context.IngredientMeals.Include(s => s.Ingredient).Include(m => m.Meal).ThenInclude(m => m.Restaurant).Where(m => m.Ingredient.Id == n);

            if (ingredientMeals == null)
            {
                return NotFound();
            }

            return Ok(ingredientMeals);
        }

        // PUT: api/IngredientMeals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredientMeal([FromRoute] int id, [FromBody] IngredientMeal ingredientMeal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingredientMeal.Id)
            {
                return BadRequest();
            }

            _context.Entry(ingredientMeal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientMealExists(id))
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

        // POST: api/IngredientMeals
        [HttpPost]
        public async Task<IActionResult> PostIngredientMeal([FromBody] IngredientMeal ingredientMeal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.IngredientMeals.Add(ingredientMeal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredientMeal", new { id = ingredientMeal.Id }, ingredientMeal);
        }

        // DELETE: api/IngredientMeals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientMeal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingredientMeal = await _context.IngredientMeals.SingleOrDefaultAsync(m => m.Id == id);
            if (ingredientMeal == null)
            {
                return NotFound();
            }

            _context.IngredientMeals.Remove(ingredientMeal);
            await _context.SaveChangesAsync();

            return Ok(ingredientMeal);
        }

        private bool IngredientMealExists(int id)
        {
            return _context.IngredientMeals.Any(e => e.Id == id);
        }
    }
}