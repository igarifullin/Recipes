using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recipe.Services.Commands;
using Recipe.Services.Models;
using Recipe.Services.Queries;

namespace Recipe.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;
        private readonly IMediator _mediator;

        public RecipesController(ILogger<RecipesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        
        [HttpGet]
        [Authorize(Roles = "Newby")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var recipes = await _mediator.Send(new GetRecipesQuery()).ConfigureAwait(false);

            return Ok(recipes);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Create(RecipeModel recipe)
        {
            await _mediator.Send(new CreateRecipeCommand
            {
                Recipe = recipe
            }).ConfigureAwait(false);

            return Accepted();
        }
        
        [Authorize(Roles = "Administrator")]
        [Route("modify")]
        [HttpPut]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Modify(RecipeModel recipe)
        {
            await _mediator.Send(new CreateRecipeCommand
            {
                Recipe = recipe
            }).ConfigureAwait(false);

            return Accepted();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Post(RecipeModel recipe)
        {
            await _mediator.Send(new UpdateRecipeCommand
            {
                Recipe = recipe
            }).ConfigureAwait(false);

            return Accepted();
        }
    }
}