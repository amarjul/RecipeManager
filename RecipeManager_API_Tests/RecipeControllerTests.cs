using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeManager_API.Controllers;
using RecipeManager_API.Data;
using RecipeManager_API.DTOs;
using RecipeManager_API.Enum;
using RecipeManager_API.Seeder;

namespace RecipeManager_API_Tests
{
    public class RecipeControllerTests
    {
        private RecipeDBContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<RecipeDBContext>()
                .UseInMemoryDatabase("TestDb") 
                .Options;

            var context = new RecipeDBContext(options);

            DbSeeder.SeedData(context);

            return context;
        }

        [Fact]
        public async Task GetRecipes_ReturnsSeedData()
        {
            var context = GetDbContext();
            var controller = new RecipesController(context);

            var result = await controller.GetRecipes(null, null, null, null);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var recipes = Assert.IsAssignableFrom<IEnumerable<RecipeDto>>(okResult.Value);

            Assert.NotEmpty(recipes);
        }

        [Fact]
        public async Task GetRecipe_InvalidId_Returns404NotFound()
        {
            var context = GetDbContext();
            var controller = new RecipesController(context);

            var result = await controller.GetRecipe(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostRecipe_Valid_Returns201Created()
        {
            var context = GetDbContext();
            var controller = new RecipesController(context);

            var dto = new CreateRecipeDto
            {
                Name = "Pizza",
                Category = "Main",
                Difficulty = DifficultyLevel.Medium,
                PreparationTime = 20,
                IsFavorite = false,
                Ingredients = new List<RecipeIngredientDto>
                {
                    new RecipeIngredientDto
                    {
                        Name = "Cheese",
                        Amount = "200g"
                    }
                }
            };

            var result = await controller.PostRecipe(dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Theory]
        [InlineData("", "Main")]
        [InlineData("Pizza", "")]
        [InlineData("", "")]
        public async Task PostRecipe_InvalidInputs_Returns400BadRequest(string name, string category)
        {
            var context = GetDbContext();
            var controller = new RecipesController(context);

            var dto = new CreateRecipeDto
            {
                Name = name,
                Category = category,
                Difficulty = DifficultyLevel.Easy,
                PreparationTime = 10,
                IsFavorite = false,
                Ingredients = new List<RecipeIngredientDto>()
            };

            var result = await controller.PostRecipe(dto);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PatchFavorite_ChangesValue()
        {
            var context = GetDbContext();
            var controller = new RecipesController(context);

            var recipe = context.Recipes.First();

            var dto = new FavoriteDto
            {
                IsFavorite = false
            };

            var result = await controller.PatchFavorite(recipe.Id, dto);

            Assert.IsType<NoContentResult>(result);

            var updated = context.Recipes.First();
            Assert.False(updated.IsFavorite);
        }
    }
}