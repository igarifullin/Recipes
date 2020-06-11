using Recipe.Services.Models;

namespace Recipe.Services.Commands
{
    public class ModifyOriginalRecipeCommand : ICommand
    {
        public RecipeModel Recipe { get; set; }
    }
}