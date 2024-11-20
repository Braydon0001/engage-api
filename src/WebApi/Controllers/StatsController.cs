using Engage.Application.Services.Stats.Models;
using Engage.Application.Services.Stats.Queries;

namespace Engage.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<StatsByEngageRegionListItemDto>>> GetAll(
        [FromRoute] GetEngageRegionStatsListQuery query) =>
             Ok(await Mediator.Send(query));

}
