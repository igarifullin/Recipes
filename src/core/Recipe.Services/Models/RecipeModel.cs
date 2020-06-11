using System.Collections.Generic;

namespace Recipe.Services.Models
{
    public class RecipeModel
    {
        public string Name { get; set; }
        
        public string Country { get; set; }
        
        public int Year { get; set; }
        
        public int TimeOfCooking { get; set; }
        
        public string Author { get; set; }
        
        public string CookingDescription { get; set; }

        public IEnumerable<IngredientModel> Ingredients { get; set; }
    }
}