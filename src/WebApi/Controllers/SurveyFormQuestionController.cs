using Engage.Application.Services.SurveyFormQuestions.Commands;
using Engage.Application.Services.SurveyFormQuestions.Queries;
using Engage.Domain.Entities.Json;
using SurveyFormQuestionOption = Engage.Application.Services.SurveyFormQuestions.Queries.SurveyFormQuestionOption;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormQuestionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SurveyFormQuestionDto>>> List([FromQuery] SurveyFormQuestionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SurveyFormQuestionDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SurveyFormQuestionOption>>> Options([FromQuery] SurveyFormQuestionOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyFormQuestionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormQuestionVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("survey/{id}")]
    public async Task<ActionResult<ListResult<SurveyFormQuestionDto>>> SurveyQuestions([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormQuestionsQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("survey/{id}/surveyFormQuestiontype/{surveyFormQuestionTypeId}")]
    public async Task<ActionResult<ListResult<SurveyFormQuestionDto>>> SurveyQuestionsByType([FromRoute] int id, [FromRoute] int surveyFormQuestionTypeId, [FromQuery] string search, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormQuestionsByTypeQuery(id, surveyFormQuestionTypeId), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("order/{groupId}")]
    public async Task<ActionResult<ListResult<SurveyFormQuestionOrderDto>>> OrderSurvey([FromRoute] int groupId, CancellationToken cancellationToken)
    {
        if (groupId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entities = await Mediator.Send(new SurveyFormQuestionOrderQuery(groupId), cancellationToken);

        return entities == null ? NotFound() : Ok(new ListResult<SurveyFormQuestionOrderDto>(entities));
    }

    [HttpGet("ruleVariableModel/{questionId}/{referenceSelf}/{excludeSelfReason}")]
    public async Task<ActionResult<VariablesWithOptions>> RuleOptions([FromRoute] int questionId, [FromRoute] bool referenceSelf, [FromRoute] bool excludeSelfReason, CancellationToken cancellationToken)
    {
        if (questionId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entities = await Mediator.Send(new SurveyFormQuestionRuleVariableModelQuery(questionId, referenceSelf, excludeSelfReason), cancellationToken);

        return entities == null ? NotFound() : Ok(entities);
    }

    [HttpGet("rule/{questionId}/{ruleType}")]
    public async Task<ActionResult<JsonRule>> Rule([FromRoute] int questionId, [FromRoute] string ruleType, CancellationToken cancellationToken)
    {
        if (questionId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entities = await Mediator.Send(new SurveyFormQuestionRuleQuery(questionId, ruleType), cancellationToken);

        return Ok(entities);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormQuestionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormQuestionId));
    }

    [HttpPost("next")]
    public async Task<IActionResult> InsertNext([FromForm] SurveyFormQuestionNextInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormQuestionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormQuestionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormQuestionId));
    }

    [HttpPut("next")]
    public async Task<IActionResult> UpdateNext([FromForm] SurveyFormQuestionNextUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormQuestionId));
    }

    [HttpPut("reorder")]
    public async Task<IActionResult> Reorder(SurveyFormQuestionReorderCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    [HttpPut("rule")]
    public async Task<IActionResult> Rule(SurveyFormQuestionRuleCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] SurveyFormQuestionFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string filename, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SurveyFormQuestionFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(filename),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

    [HttpPut("toggledisabled")]
    public override async Task<IActionResult> ToggleDisabled(DisableCommand disableCommand)
    {
        return Ok(await Mediator.Send(new SurveyFormQuestionToggleDisabledCommand
        {
            Id = disableCommand.Id,
        }));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new SurveyFormQuestionDeleteCommand
        {
            Id = id,
        }));
    }

    [HttpDelete("undo/{id}/{displayOrder}")]
    public async Task<IActionResult> UndoDelete([FromRoute] int id, [FromRoute] int displayOrder)
    {
        return Ok(await Mediator.Send(new SurveyFormQuestionUndoDeleteCommand
        {
            Id = id,
            DisplayOrder = displayOrder
        }));
    }

}
