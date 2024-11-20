using Engage.Application.Services.CreditorCutOffSettings.Commands;
using Engage.Application.Services.CreditorCutOffSettings.Queries;

namespace Engage.WebApi.Controllers;

public partial class CreditorCutOffSettingController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CreditorCutOffSettingDto>>> DtoList([FromQuery] CreditorCutOffSettingListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CreditorCutOffSettingDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreditorCutOffSettingVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CreditorCutOffSettingVmQuery(id), cancellationToken);
        if (entity == null)
        {
            return NotFound();
        }

        return entity;
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreditorCutOffSettingInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CreditorCutOffSettingId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CreditorCutOffSettingUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CreditorCutOffSettingId));
    }
}