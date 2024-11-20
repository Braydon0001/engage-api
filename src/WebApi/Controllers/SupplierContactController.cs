using Engage.Application.Services.EntityContacts.Commands;
using Engage.Application.Services.EntityContacts.Models;
using Engage.Application.Services.EntityContacts.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("Supplier")]
public class SupplierContactController : BaseController
{
    public SupplierContactController()
    {
    }

    [HttpGet()]
    [Route("supplierId/{SupplierId}")]
    [Route("supplierId/{SupplierId}/entityContactTypeId/{entityContactTypeId}")]
    public async Task<ActionResult<ListResult<SupplierContactDto>>> GetAll([FromRoute] SupplierContactsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierOption>>> OptionList([FromQuery] SupplierContactOptionsQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("options/project")]
    public async Task<ActionResult<List<SupplierContactOption>>> GetOptions([FromQuery] SupplierContactOptionByProjectQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierContactVm>> GetViewModel([FromRoute] SupplierContactVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSupplierContactCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateSupplierContactCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}