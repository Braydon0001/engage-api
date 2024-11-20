using Engage.Application.Services.StoreConceptLevels.Models;
using Engage.Application.Services.StoreConceptLevels.Queries;
using Engage.Application.Services.StoreConceptLevels.Commands;

namespace Engage.WebApi.Controllers;

public class StoreConceptLevelController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<ListResult<StoreConceptLevelDto>>> GetAll([FromQuery] StoreConceptLevelQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("storeconcepts")]
    public async Task<ActionResult<ListResult<StoreConceptVm>>> GetStoreConcepts([FromQuery] StoreConceptQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("store/{storeId}")]
    public async Task<ActionResult<ListResult<StoreConceptLevelDto>>> GetAllByStore([FromQuery] StoreConceptLevelQuery query, [FromRoute] int? StoreId)
    {
        query.StoreId = StoreId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("groupbystore")]
    public async Task<ActionResult<ListResult<StoreConceptLevelGroup>>> GroupByStore([FromQuery] StoreConceptLevelGroupQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("groupbystore/{storeId}")]
    public async Task<ActionResult<ListResult<StoreConceptLevelGroup>>> GroupBySingleStore([FromRoute] StoreConceptLevelGroupQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(StoreConceptLevelCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("batch")]
    public async Task<IActionResult> BatchCreate(StoreConceptLevelBatchCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("blob")]
    public async Task<IActionResult> CreateBlob([FromForm] StoreConceptLevelCreateBlobCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(StoreConceptLevelUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new StoreConceptLevelRemoveCommand
        {
            Id = id
        }));
    }

    [HttpDelete("blob/{id}")]
    public async Task<IActionResult> DeleteBlob([FromRoute] StoreConceptLevelDeleteBlobCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
