using Engage.Application.Services.EmployeeStoreKpiScores.Commands;
using Engage.Application.Services.EmployeeStoreKpiScores.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeStoreKpiScoreController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeStoreKpiScoreDto>>> DtoList([FromQuery] EmployeeStoreKpiScoreListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeStoreKpiScoreDto>(entities));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeStoreKpiScoreInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeStoreKpiScoreId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeStoreKpiScoreUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeStoreKpiScoreId));
    }

    [HttpPut("bulk")]
    public async Task<IActionResult> BulkUpdate([FromBody] List<EmployeeStoreKpiScoreUpdateCommand> updates, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new EmployeeStoreKpiScoreBulkUpdateCommand(updates), cancellationToken));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new EmployeeStoreKpiScoreDeleteCommand
        {
            Id = id
        }));
    }
}