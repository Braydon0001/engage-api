using Engage.Application.Services.ProjectDcProducts.Commands;
using Engage.Application.Services.ProjectDcProducts.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectDcProductController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectDcProductDto>>> List([FromQuery] ProjectDcProductListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectDcProductDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectDcProductOption>>> Options([FromQuery] ProjectDcProductOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDcProductVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectDcProductVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectDcProductInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectDcProductId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectDcProductUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectDcProductId));
    }

}
