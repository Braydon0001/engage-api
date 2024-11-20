using Engage.Application.Services.Organizations.Commands;
using Engage.Application.Services.Organizations.Queries;

namespace Engage.WebApi.Controllers;

public partial class OrganizationController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<OrganizationDto>>> List([FromQuery] OrganizationListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<OrganizationDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<OrganizationOption>>> Options([FromQuery] OrganizationOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrganizationVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new OrganizationVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(OrganizationInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.OrganizationId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(OrganizationUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrganizationId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] OrganizationFileUploadCommand command)
    {
        var file = await Mediator.Send(command);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new OrganizationFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
