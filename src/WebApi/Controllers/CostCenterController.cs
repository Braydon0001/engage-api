using Engage.Application.Services.CostCenters.Commands;
using Engage.Application.Services.CostCenters.Queries;

namespace Engage.WebApi.Controllers;

public partial class CostCenterController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CostCenterDto>>> List([FromQuery] CostCenterListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CostCenterDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CostCenterOption>>> Options([FromQuery] CostCenterOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CostCenterVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CostCenterVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CostCenterInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CostCenterId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CostCenterUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CostCenterId));
    }

}
