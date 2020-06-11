using System;
using System.Collections.Generic;
using Recipe.Data.Auth;

namespace Recipe.Data.Recipes
{
    public class RecipeVariation
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public string Country { get; set; }

        public int Year { get; set; }

        public string CookingDescription { get; set; }

        public int TimeOfCooking { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedBy { get; set; }

        public virtual User Author { get; set; }

        public virtual Recipe Recipe { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}