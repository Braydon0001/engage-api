using Engage.Application.Services.StoreConceptAttributes.Models;
using Engage.Application.Services.StoreConceptAttributes.Queries;
using Engage.Application.Services.StoreConceptAttributes.Commands;

namespace Engage.WebApi.Controllers;

public class StoreConceptAttributeController : BaseController
{
    [HttpGet("{conceptId}")]
    public async Task<ActionResult<ListResult<StoreConceptAttributeDto>>> GetByConcept([FromRoute] StoreConceptAttributeQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<StoreConceptAttributeDto>>> GetAll([FromRoute] StoreConceptAttributeQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(StoreConceptAttributeCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("batch")]
    public async Task<IActionResult> BatchCreate(StoreConceptAttributeBatchCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(StoreConceptAttributeUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new StoreConceptAttributeRemoveCommand
        {
            Id = id
        }));
    }
}
