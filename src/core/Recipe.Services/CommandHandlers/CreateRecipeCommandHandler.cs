using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recipe.Services.Commands;
using Recipe.Data;
using Recipe.Data.Recipes;

namespace Recipe.Services.CommandHandlers
{
    public class CreateRecipeCommandHandler : AsyncRequestHandler<CreateRecipeCommand>
    {
        private readonly RecipeContext _context;
        private readonly IMapper _mapper;

        public CreateRecipeCommandHandler(RecipeContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipeToCreate = _mapper.Map<Data.Recipes.Recipe>(request.Recipe);
            var recipeVariation = _mapper.Map<RecipeVariation>(request.Recipe);
            
            var existedRecipe = await _context.Recipes
                .Include(r => r.Variations)
                .FirstOrDefaultAsync(r => r.Name.Equals(recipeToCreate.Name), cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            if (existedRecipe == null)
            {
                foreach (var ingredient in recipeVariation.Ingredients)
                {
                    ingredient.RecipeVariation = recipeVariation;
                }

                var recipe = new Data.Recipes.Recipe
                {
                    Name = recipeToCreate.Name,
                    Variations = new List<RecipeVariation>
                    {
                        recipeVariation
                    }
                };

                recipeVariation.Recipe = recipe;
                await _context.Recipes.AddAsync(recipe, cancellationToken);
            }
            else
            {
                recipeVariation.RecipeId = existedRecipe.Id;
                existedRecipe.Variations.Add(recipeVariation);
            }

            await _context.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}