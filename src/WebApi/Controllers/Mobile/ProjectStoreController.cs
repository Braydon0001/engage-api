using Engage.Application.Services.Mobile.ProjectStores.Queries;
using Engage.Application.Services.Projects.Queries;
using Engage.Application.Services.ProjectStatuses.Queries;
using Engage.Application.Services.ProjectStores.Commands;
using Engage.Application.Services.ProjectStores.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class ProjectStoreController : BaseMobileController
{
    [HttpGet("store/{storeId}")]
    public async Task<ActionResult<ListResult<ProjectMobileDto>>> ProjectsByStore([FromRoute] int storeId, CancellationToken cancellationToken)
    {
        if (storeId <= 0)
        {
            return BadRequest(BadIdText);
        }
        var entities = await Mediator.Send(new ProjectStoreQuery(storeId), cancellationToken);

        return entities == null ? NotFound() : Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectStoreVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }


    [HttpGet("offline/incidents")]
    public async Task<ActionResult<ProjectVm>> OfflineProjectStoreQuery([FromQuery] OfflineProjectStoreQuery query, CancellationToken cancellationToken)
    {


        var entities = await Mediator.Send(query, cancellationToken);


        return Ok(entities);
    }

    [HttpPost("incidents")]
    public async Task<ActionResult<ListResult<ProjectDto>>> GridPaginated(ProjectStoreGridPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectDto>(entities));
    }

    [HttpPost("incidents/grouped")]
    public async Task<ActionResult<ListResult<ProjectDto>>> GridPaginatedGroup(ProjectStoreGroupedPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new Dictionary<string, List<ProjectDto>>(entities));
    }

    [AllowAnonymous]
    [HttpPost("communication")]
    public async Task<IActionResult> SendCommunication(ProjectStoreSendCommunicationCommand command, CancellationToken cancellationToken)
    {
        var optStatus = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(optStatus.Status));
    }



    [HttpPost]
    public async Task<IActionResult> Insert(ProjectStoreInsertCommand command, CancellationToken cancellationToken)
    {
        var opStatus = await Mediator.Send(command, cancellationToken);

        return Ok(opStatus);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectStoreUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    //User Specific Projects and Tasks
    [AllowAnonymous]
    [HttpGet("user/{userId}/store/{storeId}/tasks")]
    public async Task<ActionResult<ListResult<ProjectWithTasks>>> UserStoreProjectTaskList([FromRoute] int storeId, [FromRoute] int userId, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(new ProjectStoreTaskListQuery(userId, storeId), cancellationToken);

        return entities == null ? NotFound() : Ok(entities);
    }

    //Store Specific Projects and Tasks
    [AllowAnonymous]
    [HttpGet("store/{storeId}/tasks")]
    public async Task<ActionResult<ListResult<ProjectWithTasks>>> StoreProjectTaskList([FromRoute] int storeId, [FromRoute] int userId, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(new ProjectStoreTaskListQuery(userId, storeId), cancellationToken);

        return entities == null ? NotFound() : Ok(entities);
    }

    [AllowAnonymous]
    [HttpGet("store/{storeId}/task/{taskId}")]
    public async Task<ActionResult<ProjectWithTasks>> StoreProjectWithTask([FromRoute] int storeId, [FromRoute] int taskId, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(new ProjectStoreWithTaskQuery(storeId, taskId), cancellationToken);

        return entities == null ? NotFound() : Ok(entities);
    }


    [HttpGet("options/status")]
    public async Task<ActionResult<IEnumerable<ProjectStatusOption>>> StatusOptions([FromQuery] ProjectStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }


}
