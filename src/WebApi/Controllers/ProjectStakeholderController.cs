using Engage.Application.Services.ProjectStakeholders.Commands;
using Engage.Application.Services.ProjectStakeholders.Queries;

namespace Engage.WebApi.Controllers
{
    public partial class ProjectStakeholderController : BaseController
    {
        [HttpGet("projectid/{projectId}")]
        public async Task<ActionResult<ListResult<ProjectStakeholderDto>>> List([FromRoute] ProjectStakeholderListQuery query, CancellationToken cancellationToken)
        {
            var entities = await Mediator.Send(query, cancellationToken);

            return Ok(new ListResult<ProjectStakeholderDto>(entities));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProjectStakeholderInsertCommand command, CancellationToken cancellationToken)
        {
            var opStatus = await Mediator.Send(command, cancellationToken);

            return Ok(new OperationStatus(opStatus.Status));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStakeholders(ProjectStakeholderCreateCommand command, CancellationToken cancellationToken)
        {
            var opStatus = await Mediator.Send(command, cancellationToken);

            return Ok(new OperationStatus(opStatus.Status));
        }

        [HttpGet("option")]
        public async Task<ActionResult<List<OptionDto>>> GetStakheoldersByProject([FromQuery] ProjectStakeholderOptionsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("option/search")]
        public async Task<ActionResult<List<ProjectStakeholderSearchOptionDto>>> SearchStakeholderOptions([FromQuery] ProjectStakeholderSearchOptionQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet("option/user/project")]
        public async Task<ActionResult<List<OptionDto>>> GetOptionsByProject([FromQuery] ProjectStakeholderUserOptionsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("option/project")]
        public async Task<ActionResult<List<OptionDto>>> GetStakeOptionsByProject([FromQuery] ProjectStakeholderOptionsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("search/option")]
        public async Task<ActionResult<List<OptionDto>>> GetStakeholderOptions([FromQuery] ProjectStakeholderOptionsSearchQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async override Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest(BadIdText);
            }

            var entity = await Mediator.Send(new ProjectStakeholderDeleteCommand(id));

            return entity == null ? NotFound() : Ok(entity);
        }
    }
}
