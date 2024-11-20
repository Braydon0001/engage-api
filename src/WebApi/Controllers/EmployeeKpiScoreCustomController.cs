using Engage.Application.Services.EmployeeKpiScores.Commands;
using Engage.Application.Services.EmployeeKpiScores.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeKpiScoreController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeKpiScoreDto>>> DtoList([FromQuery] EmployeeKpiScoreListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeKpiScoreDto>(entities));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeKpiScoreInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeKpiScoreId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeKpiScoreUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeKpiScoreId));
    }

    [HttpPut("bulk")]
    public async Task<IActionResult> BulkUpdate([FromBody] List<EmployeeKpiScoreUpdateCommand> updates, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new EmployeeKpiScoreBulkUpdateCommand(updates), cancellationToken));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new EmployeeKpiScoreDeleteCommand
        {
            Id = id
        }));
    }
}