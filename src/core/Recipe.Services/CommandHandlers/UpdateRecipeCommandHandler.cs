using System;
using System.Linq;
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
    public class UpdateRecipeCommandHandler : AsyncRequestHandler<UpdateRecipeCommand>
    {
        private readonly RecipeContext _context;
        private readonly IMapper _mapper;

        public UpdateRecipeCommandHandler(RecipeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        protected override async Task Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipeName = request.Recipe.Name;
            var recipeVariation = _mapper.Map<RecipeVariation>(request.Recipe);

            var existedRecipe = await _context.Recipes
                .Include(r => r.Variations)
                .FirstOrDefaultAsync(r => r.Name.Equals(recipeName), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (existedRecipe == null)
            {
                throw new InvalidOperationException($"Recipe \"{recipeName}\" doesn't exist.");
            }

            var existedVariation = existedRecipe.Variations
                .FirstOrDefault(rv => rv.CreatedBy == recipeVariation.CreatedBy);

            if (existedVariation == null)
            {
                throw new InvalidOperationException($"There is no such recipe with name \"{recipeName}\" by current user. Please use Create method");
            }

            await _context.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}