using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Recipe.Data;
using Recipe.Services.Commands;

namespace Recipe.Services.CommandHandlers
{
    public class ModifyOriginalRecipeCommandHandler : AsyncRequestHandler<ModifyOriginalRecipeCommand>
    {
        private readonly RecipeContext _context;
        private readonly IMapper _mapper;

        public ModifyOriginalRecipeCommandHandler(RecipeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        protected override async Task Handle(ModifyOriginalRecipeCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}