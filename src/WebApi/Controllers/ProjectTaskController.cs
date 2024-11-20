using Engage.Application.Services.ProjectTasks.Commands;
using Engage.Application.Services.ProjectTasks.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectTaskController : BaseController
{
    [HttpGet("projectid/{projectId}")]
    public async Task<ActionResult<ListResult<ProjectTaskDto>>> List([FromRoute] ProjectTaskListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectTaskDto>(entities));
    }

    [HttpGet("board")]
    public async Task<ActionResult<Board>> TacopsBoardGet([FromQuery] ProjectTasksBoardQuery query, CancellationToken cancellationToken)
    {
        var data = await Mediator.Send(query, cancellationToken);

        return data == null ? NotFound() : Ok(data);
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectTaskOption>>> Options([FromQuery] ProjectTaskOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectTaskVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectTaskVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("summary/{id}")]
    public async Task<ActionResult<ProjectTaskSummaryVm>> SummaryVm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectTaskSummaryVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectTaskInsertCommand command, CancellationToken cancellationToken)
    {
        var opStatus = await Mediator.Send(command, cancellationToken);

        return Ok(opStatus);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectTaskUpdateCommand command, CancellationToken cancellationToken)
    {
        var opStatus = await Mediator.Send(command, cancellationToken);

        return Ok(opStatus);
    }

    [HttpPut("taskname")]
    public async Task<IActionResult> UpdateName(ProjectTaskNameUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectTaskId));
    }

    [HttpPut("note")]
    public async Task<IActionResult> UpdateNote(ProjectTaskNoteUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectTaskId));
    }

    [HttpPut("estimatedhours")]
    public async Task<IActionResult> UpdateEstimatedHours(ProjectTaskEstimatedHoursUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectTaskId));
    }

    [HttpPut("remaininghours")]
    public async Task<IActionResult> UpdateRemainingHours(ProjectTaskRemainingHoursUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectTaskId));
    }

    [HttpPut("userid")]
    public async Task<IActionResult> UpdateEmployeeId(ProjectTaskUserIdUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectTaskId));
    }

    [HttpDelete("{id}")]
    public async override Task<IActionResult> Delete([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectTaskDeleteCommand(id));

        return entity == null ? NotFound() : Ok(new OperationStatus(id));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] ProjectTaskFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file/{id}/name/{name}")]
    [HttpDelete("file/{id}/name/{name}/type/{type}")]
    public async Task<ActionResult> FileDelete([FromRoute] int id, [FromRoute] string name, [FromRoute] string type, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new ProjectTaskFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(name),
            FileType = !string.IsNullOrWhiteSpace(type) ? HttpUtility.UrlDecode(type) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
