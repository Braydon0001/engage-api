using Engage.Application.Services.StoreStoreConcepts.Models;
using Engage.Application.Services.StoreStoreConcepts.Queries;
using Engage.Application.Services.StoreStoreConcepts.Commands;

namespace Engage.WebApi.Controllers;

public class StoreStoreConceptController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<ListResult<StoreStoreConceptDto>>> GetAll([FromQuery] StoreStoreConceptsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("store/{storeId}")]
    public async Task<ActionResult<ListResult<StoreStoreConceptDto>>> GetAllByStore([FromQuery] StoreStoreConceptsQuery query, [FromRoute] int? StoreId)
    {
        query.StoreId = StoreId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("groupbystore")]
    public async Task<ActionResult<ListResult<StoreConceptsGroup>>> GroupByStore([FromQuery] StoreConceptsGroupQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(StoreStoreConceptCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(StoreStoreConceptUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new StoreStoreConceptRemoveCommand
        {
            Id = id
        }));
    }
}
