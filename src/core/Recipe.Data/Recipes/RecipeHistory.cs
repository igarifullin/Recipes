using System;
using Recipe.Data.Auth;

namespace Recipe.Data.Recipes
{
    public class RecipeHistory
    {
        public int Id { get; set; }
        
        public Guid ChangedBy { get; set; }
        
        public int RecipeId { get; set; }
        
        public virtual Recipe Recipe { get; set; }
        
        public virtual User Changer { get; set; }
    }
}