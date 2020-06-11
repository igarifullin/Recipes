using System;
using System.Collections.Generic;
using Recipe.Data.Recipes;

namespace Recipe.Data.Auth
{
    public class User
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }
        
        public string Name { get; set; }

        public int RoleId { get; set; }

        public virtual UserRole Role { get; set; }

        public virtual ICollection<RecipeVariation> Recipes { get; set; }
        
        public virtual ICollection<RecipeHistory> RecipeChanges { get; set; }
    }
}