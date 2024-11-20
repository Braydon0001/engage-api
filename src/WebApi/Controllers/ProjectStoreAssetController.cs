using Engage.Application.Services.ProjectStoreAssets.Commands;
using Engage.Application.Services.ProjectStoreAssets.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectStoreAssetController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectStoreAssetDto>>> List([FromQuery] ProjectStoreAssetListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectStoreAssetDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectStoreAssetOption>>> Options([FromQuery] ProjectStoreAssetOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectStoreAssetVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectStoreAssetVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectStoreAssetInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectStoreAssetId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectStoreAssetUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectStoreAssetId));
    }

}
