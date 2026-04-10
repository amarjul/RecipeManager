namespace RecipeManager_API.DTOs
{
    public class RecipeIngredientDto
    {
        // Keine unnoetigen interne Entity-Details -> IDs nicht notwendig 
        public string? Name { get; set; }   
        public string? Amount { get; set; } 
    }
}
