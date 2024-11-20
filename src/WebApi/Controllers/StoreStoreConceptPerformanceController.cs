using Engage.Application.Services.StoreStoreConceptPerformances.Models;
using Engage.Application.Services.StoreStoreConceptPerformances.Queries;

namespace Engage.WebApi.Controllers;

public class StoreStoreConceptPerformanceController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<ListResult<StoreStoreConceptPerformanceDto>>> GetQuery([FromQuery] StoreStoreConceptPerformanceQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
