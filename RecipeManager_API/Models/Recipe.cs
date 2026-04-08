using RecipeManager_API.Enum;

namespace RecipeManager_API.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public DifficultyLevel Difficulty {  get; set; }

        public int PreparationTime { get; set; }

        public bool IsFavorite { get; set; }

        public List<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
    }
}
