using Engage.Application.Services.PaymentLineFiles.Commands;
using Engage.Application.Services.PaymentLineFiles.Queries;

namespace Engage.WebApi.Controllers;

public partial class PaymentLineFileController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<PaymentLineFileDto>>> List([FromQuery] PaymentLineFileListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<PaymentLineFileDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<PaymentLineFileOption>>> Options([FromQuery] PaymentLineFileOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentLineFileVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new PaymentLineFileVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(PaymentLineFileInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.PaymentLineFileId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(PaymentLineFileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.PaymentLineFileId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] PaymentLineFileFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file/{id}/name/{name}")]
    [HttpDelete("file/{id}/name/{name}/type/{type}")]
    public async Task<ActionResult> FileDelete([FromRoute] int id, [FromRoute] string name, [FromRoute] string type, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new PaymentLineFileFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(name),
            FileType = !string.IsNullOrWhiteSpace(type) ? HttpUtility.UrlDecode(type) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
