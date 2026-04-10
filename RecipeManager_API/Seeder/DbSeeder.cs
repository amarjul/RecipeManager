using RecipeManager_API.Data;
using RecipeManager_API.Enum;
using RecipeManager_API.Models;

namespace RecipeManager_API.Seeder
{
    public static class DbSeeder
    {
        public static void SeedData(RecipeDBContext context)
        {
            // Ueberpruefung, ob bereits Daten vorhanden sind (Vermeidung doppelter Daten)
            if(context.Recipes.Any())
            {
                return;
            }

            // Liste mit Recipes
            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Name = "Doener Box",
                    Category = "Hauptspeisse",
                    Difficulty = DifficultyLevel.Medium,
                    PreparationTime = 10,
                    IsFavorite = true,
                    Ingredients = new List<RecipeIngredient>
                    {
                            new RecipeIngredient
                            {
                                Name = "Kebabfleisch",
                                Amount = "200g"
                            },
                            new RecipeIngredient
                            {
                                Name = "Pommes",
                                Amount = "200g"
                            },
                            new RecipeIngredient
                            {
                                Name = "Souce",
                                Amount = "50g"
                            }
                    }
                    
                },
                new Recipe
                {
                    Name = "Omlette",
                    Category = "Fruehstueck",
                    Difficulty = DifficultyLevel.Easy,
                    PreparationTime = 10,
                    IsFavorite = true,
                    Ingredients = new List<RecipeIngredient>
                    {
                            new RecipeIngredient
                            {
                                Name = "Eier",
                                Amount = "4 Stk."
                            },
                            new RecipeIngredient
                            {
                                Name = "Salz",
                                Amount = "1 Prise"
                            },
                            new RecipeIngredient
                            {
                                Name = "Milch",
                                Amount = "10ml"
                            }
                    }
                },
                new Recipe
                {
                    Name = "Steak",
                    Category = "Anlassessen",
                    Difficulty = DifficultyLevel.Hard,
                    PreparationTime = 10,
                    IsFavorite = true,
                    Ingredients = new List<RecipeIngredient>
                    {
                            new RecipeIngredient
                            {
                                Name = "Entrecoute",
                                Amount = "500g"
                            },
                            new RecipeIngredient
                            {
                                Name = "Butter",
                                Amount = "20g"
                            },
                            new RecipeIngredient
                            {
                                Name = "Knoblauch",
                                Amount = "1 Zehe"
                            },
                            new RecipeIngredient
                            {
                                Name = "Rosmarin",
                                Amount = "10g"
                            }
                    }
                }
            };

            // Hinzufuegen mehrerer Recipes
            context.Recipes.AddRange(recipes);
            
            context.SaveChanges();
        }
    }
}
