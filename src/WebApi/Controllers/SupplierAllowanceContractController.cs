using Engage.Application.Services.SupplierAllowanceContracts.Commands;
using Engage.Application.Services.SupplierAllowanceContracts.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierAllowanceContractController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierAllowanceContractDto>>> DtoList([FromQuery] SupplierAllowanceContractListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierAllowanceContractDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierAllowanceContractVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierAllowanceContractVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierAllowanceContractInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierAllowanceContractId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierAllowanceContractUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierAllowanceContractId));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] SupplierAllowanceContractUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SupplierAllowanceContractDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}