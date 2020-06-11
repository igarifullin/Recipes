using System.Collections.Generic;

namespace Recipe.Data.Recipes
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<RecipeVariation> Variations { get; set; }
        
        public ICollection<RecipeHistory> History { get; set; }
    }
}