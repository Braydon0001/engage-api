using Engage.Application.Services.Mobile.ProjectStores.Queries;
using Engage.Application.Services.ProjectTasks.Queries;

namespace Engage.WebApi.Controllers.Mobile
{
    public partial class UserTaskController : BaseMobileController
    {
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ListResult<ProjectTaskDto>>> ProjectsByStore([FromRoute] int userId, CancellationToken cancellationToken)
        {
            if (userId <= 0)
            {
                return BadRequest(BadIdText);
            }
            var entities = await Mediator.Send(new ProjectTasksQuery { UserId = userId }, cancellationToken);

            return entities == null ? NotFound() : Ok(entities);
        }

    }
}
