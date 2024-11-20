using Engage.Application.Services.UserEntities.Models;
using Engage.Application.Services.UserEntitities.Queries;

namespace Engage.WebApi.Controllers;

public class UserEntityController : BaseController
{
    [HttpGet("entity/{entity}")]
    public async Task<ActionResult<ListResult<UserEntityDto>>> GetAll([FromQuery] UserEntitiesQuery query, [FromRoute] string entity)
    {
        query.Entity = entity;
        return Ok(await Mediator.Send(query));
    }
}
