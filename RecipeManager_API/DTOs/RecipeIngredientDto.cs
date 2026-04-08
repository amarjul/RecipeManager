namespace RecipeManager_API.DTOs
{
    public class RecipeIngredientDto
    {
        public string Name { get; set; } = string.Empty;    //string? kann zu Null-Problemen fuehren
        public string Amount { get; set; } = string.Empty;
    }
}
