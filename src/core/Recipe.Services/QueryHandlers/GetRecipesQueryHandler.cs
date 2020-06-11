using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recipe.Services.Models;
using Recipe.Services.Queries;
using Recipe.Data;

namespace Recipe.Services.QueryHandlers
{
    public class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, IEnumerable<RecipeModel>>
    {
        private readonly RecipeContext _context;
        private readonly IMapper _mapper;

        public GetRecipesQueryHandler(RecipeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<RecipeModel>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
        {
            var recipes = await _context.RecipeVariations
                .Include(rv => rv.Author)
                .Include(rv => rv.Ingredients)
                .Include(rv => rv.Recipe)
                .OrderByDescending(rv => rv.CreatedAt)
                .Take(100)
                .ToArrayAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<RecipeModel>>(recipes);
        }
    }
}