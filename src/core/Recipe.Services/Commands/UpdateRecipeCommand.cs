using MediatR;
using Recipe.Services.Models;

namespace Recipe.Services.Commands
{
    public class UpdateRecipeCommand : ICommand
    {
        public RecipeModel Recipe { get; set; }        
    }
}