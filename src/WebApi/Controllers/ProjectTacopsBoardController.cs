using Engage.Application.Services.ProjectTacopsBoard.Commands;
using Engage.Application.Services.ProjectTacopsBoard.Queries;

namespace Engage.WebApi.Controllers
{
    public partial class ProjectTacopsBoardController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Board>> TacopsBoardGet([FromQuery] ProjectStoreTacopsBoardQuery query, CancellationToken cancellationToken)
        {
            var data = await Mediator.Send(query, cancellationToken);

            return data == null ? NotFound() : Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<Board>> TacopsBoard(ProjectStoreTacopsBoardQuery query, CancellationToken cancellationToken)
        {
            var data = await Mediator.Send(query, cancellationToken);

            return data == null ? NotFound() : Ok(data);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Insert(ProjectStoreTacopsBoardUpdateCommand command, CancellationToken cancellationToken)
        {
            var opStatus = await Mediator.Send(command, cancellationToken);

            return Ok(opStatus);
        }

        [HttpPut("task/summary")]
        public async Task<IActionResult> UpdateTask(ProjectTaskUpdateSummaryCommand command, CancellationToken cancellationToken)
        {
            var entity = await Mediator.Send(command, cancellationToken);

            return entity == null ? NotFound() : Ok(entity);
        }
    }
}
