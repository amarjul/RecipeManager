using RecipeManager_API.Enum;

namespace RecipeManager_API.DTOs
{
    public class CreateRecipeDto
    {
        public string? Name { get; set; }

        public string? Category { get; set; }

        public DifficultyLevel Difficulty { get; set; }

        public int PreparationTime { get; set; }

        public bool IsFavorite { get; set; }

        public List<RecipeIngredientDto> Ingredients { get; set; } = new List<RecipeIngredientDto>();
    }
}

