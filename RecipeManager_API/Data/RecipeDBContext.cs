using Microsoft.EntityFrameworkCore;
using RecipeManager_API.Models;

namespace RecipeManager_API.Data
{
    public class RecipeDBContext : DbContext
    {
        public RecipeDBContext(DbContextOptions<RecipeDBContext> options)
            : base(options)
        {
        }

        // Tabellen fuer Recipes und die Ingredients
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
