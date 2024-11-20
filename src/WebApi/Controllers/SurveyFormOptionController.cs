using Engage.Application.Services.SurveyFormOptions.Commands;
using Engage.Application.Services.SurveyFormOptions.Queries;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormOptionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SurveyFormOptionDto>>> List([FromQuery] SurveyFormOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SurveyFormOptionDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SurveyFormOptionOption>>> Options([FromQuery] SurveyFormOptionOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyFormOptionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormOptionVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormOptionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormOptionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormOptionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormOptionId));
    }

}
