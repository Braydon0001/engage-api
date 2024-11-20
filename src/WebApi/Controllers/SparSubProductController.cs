using Engage.Application.Services.SparSubProducts.Commands;
using Engage.Application.Services.SparSubProducts.Queries;

namespace Engage.WebApi.Controllers;

public partial class SparSubProductController : BaseController
{
    [HttpGet("sparproduct/{sparProductId}")]
    public async Task<ActionResult<ListResult<SparSubProductDto>>> List([FromRoute] SparSubProductByProductQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);
        return Ok(new ListResult<SparSubProductDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SparSubProductVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SparSubProductVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SparSubProductInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SparSubProductId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SparSubProductUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SparSubProductId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] SparSubProductFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SparSubProductFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
