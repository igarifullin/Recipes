using System.Collections.Generic;
using Recipe.Services.Models;

namespace Recipe.Services.Queries
{
    public class GetRecipesQuery : IQuery<IEnumerable<RecipeModel>>
    {
    }
}