using Engage.Application.Services.SupplierAllowanceSubContracts.Commands;
using Engage.Application.Services.SupplierAllowanceSubContracts.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierAllowanceSubContractController : BaseController
{
    [HttpGet("supplierallowancecontract/{supplierallowancecontractid}")]
    public async Task<ActionResult<ListResult<SupplierAllowanceSubContractDto>>> DtoList([FromRoute] int supplierAllowanceContractId, CancellationToken cancellationToken)
    {
        if (supplierAllowanceContractId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entities = await Mediator.Send(new SupplierAllowanceSubContractListQuery { SupplierAllowanceContractId = supplierAllowanceContractId }, cancellationToken);

        return Ok(new ListResult<SupplierAllowanceSubContractDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierAllowanceSubContractVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierAllowanceSubContractVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierAllowanceSubContractInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierAllowanceSubContractId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierAllowanceSubContractUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierAllowanceSubContractId));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] SupplierAllowanceSubContractUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SupplierAllowanceSubContractDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}