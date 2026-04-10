namespace RecipeManager_API.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Amount { get; set; }
        public int RecipeID { get; set; }
    }
}
