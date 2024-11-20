using Engage.Application.Services.StoreConceptAttributeValues.Commands;
using Engage.Application.Services.StoreConceptAttributeValues.Models;
using Engage.Application.Services.StoreConceptAttributeValues.Queries;

namespace Engage.WebApi.Controllers;

public class StoreConceptAttributeValueController : BaseController
{
    [HttpGet("sId/{storeId}/cId/{storeConceptId}")]
    public async Task<ActionResult<ListResult<StoreConceptAttributeValueDto>>> GetAll([FromRoute] StoreConceptAttributeValueQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("storeId/{storeId}/conceptId/{storeConceptId}")]
    public async Task<ActionResult<ListResult<StoreConceptAttributeValueGroup>>> GetConceptByStore([FromRoute] StoreConceptAttributeValueGroupQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<StoreConceptAttributeValueVm>> GetConceptValue([FromRoute] StoreConceptAttributeValueVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(StoreConceptAttributeValueCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(StoreConceptAttributeValueUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("bulk")]
    public async Task<IActionResult> BulkUpdate(StoreConceptAttributeValueBulkUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new StoreConceptAttributeValueRemoveCommand
        {
            Id = id
        }));
    }
}
