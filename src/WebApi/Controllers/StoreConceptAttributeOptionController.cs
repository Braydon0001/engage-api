using Engage.Application.Services.StoreConceptAttributeOptions.Models;
using Engage.Application.Services.StoreConceptAttributeOptions.Queries;
using Engage.Application.Services.StoreConceptAttributeOptions.Commands;

namespace Engage.WebApi.Controllers;

public class StoreConceptAttributeOptionController : BaseController
{
    [HttpGet("{StoreConceptAttributeId}")]
    public async Task<ActionResult<ListResult<StoreConceptAttributeOptionDto>>> GetByConcept([FromRoute] StoreConceptAttributeOptionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<StoreConceptAttributeOptionDto>>> GetAll([FromRoute] StoreConceptAttributeOptionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(StoreConceptAttributeOptionCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("batch")]
    public async Task<IActionResult> BatchCreate(StoreConceptAttributeOptionBatchCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(StoreConceptAttributeOptionUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new StoreConceptAttributeOptionRemoveCommand
        {
            Id = id
        }));
    }
}
