// auto-generated
using Engage.Application.Services.SupplierContractDetails.Commands;
using Engage.Application.Services.SupplierContractDetails.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierContractDetailController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierContractDetailDto>>> DtoList([FromQuery] SupplierContractDetailListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierContractDetailDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierContractDetailOption>>> OptionList([FromQuery] SupplierContractDetailOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierContractDetailVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierContractDetailVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierContractDetailInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierContractDetailId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierContractDetailUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierContractDetailId));
    }

    [HttpPut("bulk")]
    public async Task<IActionResult> BulkUpdate([FromBody] List<SupplierContractDetailUpdateCommand> updates, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new SupplierContractDetailBulkUpdateCommand(updates), cancellationToken));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] SupplierContractDetailFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new SupplierContractDetailDeleteCommand
        {
            Id = id
        }));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SupplierContractDetailFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }
}