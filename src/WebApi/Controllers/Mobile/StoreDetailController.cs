using Engage.Application.Services.EntityContacts.Commands;
using Engage.Application.Services.EntityContacts.Models;
using Engage.Application.Services.EntityContacts.Queries;
using Engage.Application.Services.Locations.Commands;
using Engage.Application.Services.Locations.Models;
using Engage.Application.Services.Locations.Queries;

namespace Engage.WebApi.Controllers.Mobile;

[Authorize("store")]
public class StoreDetailController : BaseMobileController
{
    [AllowAnonymous]
    [HttpGet("contacts/storeId/{storeId}")]

    public async Task<ActionResult<ListResult<StoreContactDto>>> GetAll([FromRoute] StoreContactsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options/email/storeId/{storeId}")]
    public async Task<ActionResult<ListResult<StoreContactEmailOption>>> ContactOptions([FromRoute] StoreContactOptionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreContactVm>> GetViewModel([FromRoute] StoreContactVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("address/storeId/{storeId}")]
    public async Task<ActionResult<ListResult<LocationListItemDto>>> GetLocations([FromRoute] int storeId)
    {
        return Ok(await Mediator.Send(new LocationsQuery
        {
            StakeholderType = StakeholderTypes.Store,
            EntityId = storeId,
        }));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStoreContactCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    [AllowAnonymous]
    [HttpPut("contactupdate")]
    public async Task<IActionResult> Update(UpdateStoreContactCommand command)
    {
        return Ok(await Mediator.Send(command));
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new StoreContactFileDeleteCommand
        {
            Id = id
        }));
    }

    [HttpPut("addressupdate")]

    public async Task<IActionResult> Update(UpdateLocationCommand command)
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
