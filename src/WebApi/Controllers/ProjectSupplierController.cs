using Engage.Application.Services.ProductSuppliers.Queries;
using Engage.Application.Services.ProjectSuppliers.Commands;
using Engage.Application.Services.ProjectSuppliers.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectSupplierController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectSupplierDto>>> List([FromQuery] ProjectSupplierListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectSupplierDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectSupplierOption>>> Options([FromQuery] ProjectSupplierOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("category/options")]
    public async Task<ActionResult<IEnumerable<ProductSupplierOption>>> Supplier([FromQuery] ProjectSupplierSearchOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectSupplierVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectSupplierVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectSupplierInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectSupplierId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectSupplierUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectSupplierId));
    }

}
