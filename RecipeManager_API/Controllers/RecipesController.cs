using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeManager_API.Data;
using RecipeManager_API.DTOs;
using RecipeManager_API.Models;

namespace RecipeManager_API.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipeDBContext _context;

        public RecipesController(RecipeDBContext context)
        {
            _context = context;
        }

        // GET: api/recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipes(
            string? category,
            int? difficulty,
            bool? isFavorite,
            int? maxPreparationTime)
        {
            var query = _context.Recipes
                .Include(r => r.Ingredients)
                .AsQueryable();

            var recipes = await query.ToListAsync();

            var result = recipes.Select(r => new RecipeDto
            {
                Id = r.Id,
                Name = r.Name,
                Category = r.Category,
                Difficulty = r.Difficulty,
                PreparationTime = r.PreparationTime,
                IsFavorite = r.IsFavorite,
                Ingredients = r.Ingredients.Select(i => new RecipeIngredientDto
                {
                    Name = i.Name,
                    Amount = i.Amount
                }).ToList()
            }).ToList();

            return Ok(result);
        }

        // GET: api/recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            var result = new RecipeDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Category = recipe.Category,
                Difficulty = recipe.Difficulty,
                PreparationTime = recipe.PreparationTime,
                IsFavorite = recipe.IsFavorite,
                Ingredients = recipe.Ingredients.Select(i => new RecipeIngredientDto
                {
                    Name = i.Name,
                    Amount = i.Amount
                }).ToList()
            };

            return Ok(result);
        }

        // POST: api/recipes
        [HttpPost]
        public async Task<ActionResult<RecipeDto>> PostRecipe(CreateRecipeDto dto)
        {
            var recipe = new Recipe
            {
                Name = dto.Name,
                Category = dto.Category,
                Difficulty = dto.Difficulty,
                PreparationTime = dto.PreparationTime,
                IsFavorite = dto.IsFavorite,
                Ingredients = dto.Ingredients.Select(i => new RecipeIngredient
                {
                    Name = i.Name,
                    Amount = i.Amount
                }).ToList()
            };

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            var result = new RecipeDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Category = recipe.Category,
                Difficulty = recipe.Difficulty,
                PreparationTime = recipe.PreparationTime,
                IsFavorite = recipe.IsFavorite,
                Ingredients = recipe.Ingredients.Select(i => new RecipeIngredientDto
                {
                    Name = i.Name,
                    Amount = i.Amount
                }).ToList()
            };

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, result);
        }

        // PUT: api/recipes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, UpdateRecipeDto dto)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            recipe.Name = dto.Name;
            recipe.Category = dto.Category;
            recipe.Difficulty = dto.Difficulty;
            recipe.PreparationTime = dto.PreparationTime;
            recipe.IsFavorite = dto.IsFavorite;

            recipe.Ingredients.Clear();

            foreach (var i in dto.Ingredients)
            {
                recipe.Ingredients.Add(new RecipeIngredient
                {
                    Name = i.Name,
                    Amount = i.Amount
                });
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/recipes/5/favorite
        [HttpPatch("{id}/favorite")]
        public async Task<IActionResult> PatchFavorite(int id, FavoriteDto dto)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            recipe.IsFavorite = dto.IsFavorite;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}