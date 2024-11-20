using Engage.Application.Services.CategoryTargetAnswers.Commands;
using Engage.Application.Services.CategoryTargetAnswers.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class CategoryTargetAnswerController : BaseMobileController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryTargetAnswerVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CategoryTargetAnswerVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("storeId/{storeId}")]
    public async Task<ActionResult<ListResult<CategoryTargetAnswerDto>>> GetById([FromRoute] CategoryTargetAnswerListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CategoryTargetAnswerDto>(entities));
    }


    [HttpPost("page")]
    public async Task<ActionResult<ListResult<CategoryTargetAnswerDto>>> Paginated(CategoryTargetAnswerPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CategoryTargetAnswerDto>(entities));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CategoryTargetAnswerInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CategoryTargetAnswerId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryTargetAnswerUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CategoryTargetAnswerId));
    }

    [HttpPut("verifyHistory")]
    public async Task<IActionResult> UpdateHistory(CategoryTargetAnswerUpdateHistoryCommand command, CancellationToken cancellationToken)
    {
        var opStatus = await Mediator.Send(command, cancellationToken);

        return opStatus == null ? NotFound() : Ok(opStatus);
    }
}
