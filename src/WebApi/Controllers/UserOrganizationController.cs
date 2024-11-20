using Engage.Application.Services.UserOrganizations.Commands;
using Engage.Application.Services.UserOrganizations.Queries;

namespace Engage.WebApi.Controllers;

public partial class UserOrganizationController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<UserOrganizationDto>>> List([FromQuery] UserOrganizationListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<UserOrganizationDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<UserOrganizationOption>>> Options([FromQuery] UserOrganizationOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserOrganizationVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new UserOrganizationVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(UserOrganizationInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.UserOrganizationId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserOrganizationUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.UserOrganizationId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] UserOrganizationFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file/{id}/name/{name}")]
    [HttpDelete("file/{id}/name/{name}/type/{type}")]
    public async Task<ActionResult> FileDelete([FromRoute] int id, [FromRoute] string name, [FromRoute] string type, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new UserOrganizationFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(name),
            FileType = !string.IsNullOrWhiteSpace(type) ? HttpUtility.UrlDecode(type) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
