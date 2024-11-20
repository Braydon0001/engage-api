using Engage.Application.Services.ProjectCategorySuppliers.Commands;
using Engage.Application.Services.ProjectCategorySuppliers.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectCategorySupplierController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectCategorySupplierDto>>> List([FromQuery] ProjectCategorySupplierListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectCategorySupplierDto>(entities));
    }

    [HttpGet("category/options")]
    public async Task<ActionResult<IEnumerable<ProjectCategorySupplierOption>>> Options([FromQuery] ProjectCategorySupplierOptionQuery query, CancellationToken cancellationToken)
    {  
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectCategorySupplierVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectCategorySupplierVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectCategorySupplierInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectCategorySupplierId));
    }

}
