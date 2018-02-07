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
    [Route("api/Ingredients")]
    [ApiVersion("1.0")]
    [Authorize]
    public class IngredientsController : Controller
    {
        private readonly RestaurantsContext _context;

        public IngredientsController(RestaurantsContext context)
        {
            _context = context;
        }

        // GET: api/Ingredients
        [HttpGet]
        public IEnumerable<Ingredient> GetIngredient()
        {
            return _context.Ingredients;
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingredient = await _context.Ingredients.SingleOrDefaultAsync(m => m.Id == id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return Ok(ingredient);
        }

        // PUT: api/Ingredients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient([FromRoute] int id, [FromBody] Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingredient.Id)
            {
                return BadRequest();
            }

            _context.Entry(ingredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
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

        // POST: api/Ingredients
        [HttpPost]
        public async Task<IActionResult> PostIngredient([FromBody] Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSastojak", new { id = ingredient.Id }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sastojak = await _context.Ingredients.SingleOrDefaultAsync(m => m.Id == id);
            if (sastojak == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(sastojak);
            await _context.SaveChangesAsync();

            return Ok(sastojak);
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(e => e.Id == id);
        }
    }
}