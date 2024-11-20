using Engage.Application.Services.ProjectCampaigns.Commands;
using Engage.Application.Services.ProjectCampaigns.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectCampaignController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectCampaignVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectCampaignVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectCampaignOption>>> Options([FromQuery] ProjectCampaignOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectCampaignInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectCampaignId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectCampaignUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectCampaignId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] ProjectCampaignFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file/{id}/name/{name}")]
    [HttpDelete("file/{id}/name/{name}/type/{type}")]
    public async Task<ActionResult> FileDelete([FromRoute] int id, [FromRoute] string name, [FromRoute] string type, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new ProjectCampaignFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(name),
            FileType = !string.IsNullOrWhiteSpace(type) ? HttpUtility.UrlDecode(type) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
