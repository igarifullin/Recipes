using Recipe.Services.Models;

namespace Recipe.Services.Commands
{
    public class CreateRecipeCommand : ICommand
    {
        public RecipeModel Recipe { get; set; }
    }
}