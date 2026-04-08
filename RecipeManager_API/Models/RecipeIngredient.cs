namespace RecipeManager_API.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;  // oder string?
        public int RecipeID { get; set; }
    }
}
