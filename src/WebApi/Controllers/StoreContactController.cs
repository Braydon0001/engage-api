using Engage.Application.Services.EntityContacts.Commands;
using Engage.Application.Services.EntityContacts.Models;
using Engage.Application.Services.EntityContacts.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("store")]
public class StoreContactController : BaseController
{
    [HttpGet("storeId/{storeId}")]

    public async Task<ActionResult<ListResult<StoreContactDto>>> GetAll([FromRoute] StoreContactsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<ListResult<StoreContactOption>>> StoreContactOptions([FromQuery] StoreContactOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("options/project")]
    public async Task<ActionResult<List<StoreContactOption>>> GetOptions([FromQuery] StoreContactOptionByProjectQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("options/email/storeId/{storeId}")]
    public async Task<ActionResult<ListResult<StoreContactEmailOption>>> ContactOptions([FromRoute] StoreContactEmailOptionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreContactVm>> GetViewModel([FromRoute] StoreContactVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStoreContactCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateStoreContactCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] StoreContactFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new StoreContactFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
