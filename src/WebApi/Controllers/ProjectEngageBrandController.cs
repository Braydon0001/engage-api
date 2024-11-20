using Engage.Application.Services.ProjectEngageBrands.Commands;
using Engage.Application.Services.ProjectEngageBrands.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectEngageBrandController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectEngageBrandDto>>> List([FromQuery] ProjectEngageBrandListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectEngageBrandDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectEngageBrandVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectEngageBrandVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectEngageBrandUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

}
