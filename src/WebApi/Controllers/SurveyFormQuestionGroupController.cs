using Engage.Application.Services.SurveyFormQuestionGroups.Commands;
using Engage.Application.Services.SurveyFormQuestionGroups.Queries;
using Engage.Application.Services.SurveyFormQuestions.Queries;
using Engage.Domain.Entities.Json;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormQuestionGroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SurveyFormQuestionGroupDto>>> List([FromQuery] SurveyFormQuestionGroupListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SurveyFormQuestionGroupDto>(entities));
    }

    [HttpGet("survey/{surveyId}")]
    public async Task<ActionResult<ListResult<SurveyFormQuestionGroupDto>>> ListSurvey([FromRoute] int surveyId, CancellationToken cancellationToken)
    {
        if (surveyId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entities = await Mediator.Send(new SurveyQuestionGroupListQuery(surveyId), cancellationToken);

        return entities == null ? NotFound() : Ok(new ListResult<SurveyFormQuestionGroupDto>(entities));
    }

    [HttpGet("order/{surveyId}")]
    public async Task<ActionResult<ListResult<SurveyFormQuestionGroupOrderDto>>> OrderSurvey([FromRoute] int surveyId, CancellationToken cancellationToken)
    {
        if (surveyId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entities = await Mediator.Send(new SurveyQuestionGroupOrderQuery(surveyId), cancellationToken);

        return entities == null ? NotFound() : Ok(new ListResult<SurveyFormQuestionGroupOrderDto>(entities));
    }

    [HttpGet("ruleVariableModel/{groupId}")]
    public async Task<ActionResult<VariablesWithOptions>> RuleOptions([FromRoute] int groupId, CancellationToken cancellationToken)
    {
        if (groupId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entities = await Mediator.Send(new SurveyFormQuestionGroupRuleVariableModelQuery(groupId), cancellationToken);

        return entities == null ? NotFound() : Ok(entities);
    }

    [HttpGet("rule/{groupId}/{ruleType}")]
    public async Task<ActionResult<JsonRule>> Rule([FromRoute] int groupId, [FromRoute] string ruleType, CancellationToken cancellationToken)
    {
        if (groupId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entities = await Mediator.Send(new SurveyFormQuestionGroupRuleQuery(groupId, ruleType), cancellationToken);

        return Ok(entities);
    }

    [HttpGet("surveyquestions/{surveyId}")]
    public async Task<ActionResult<ListResult<SurveyFormQuestionGroupDto>>> ListSurveyMasterDetail([FromRoute] int surveyId, CancellationToken cancellationToken)
    {
        if (surveyId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entities = await Mediator.Send(new SurveyFormQuestionGroupMasterDetailQuery(surveyId), cancellationToken);

        return entities == null ? NotFound() : Ok(new ListResult<SurveyFormQuestionGroupDto>(entities));
    }

    [HttpGet("options/{surveyId}")]
    public async Task<ActionResult<IEnumerable<SurveyFormQuestionGroupOption>>> Options([FromRoute] int surveyId, [FromQuery] SurveyFormQuestionGroupOptionQuery query, CancellationToken cancellationToken)
    {
        if (surveyId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entities = await Mediator.Send(new SurveyFormQuestionGroupOptionQuery(surveyId), cancellationToken);

        return entities == null ? NotFound() : Ok(new List<SurveyFormQuestionGroupOption>(entities));
    }

    [HttpGet("{groupId}")]
    public async Task<ActionResult<SurveyFormQuestionGroupVm>> Vm([FromRoute] int groupId, CancellationToken cancellationToken)
    {
        if (groupId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormQuestionGroupVmQuery(groupId), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormQuestionGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormQuestionGroupId));
    }

    [HttpPost("next")]
    public async Task<IActionResult> InsertNext([FromForm] SurveyFormQuestionGroupNextInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormQuestionGroupId));
    }

    [HttpPost("shelfspacingsurvey")]
    public async Task<IActionResult> InsertShelfSpacing(SurveyFormQuestionGroupShelfSpacingInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormQuestionGroupId));
    }

    [HttpPut("shelfspacingsurvey")]
    public async Task<IActionResult> UpdateShelfSpacing(SurveyFormQuestionGroupShelfSpacingUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormQuestionGroupId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormQuestionGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormQuestionGroupId));
    }

    [HttpPut("next")]
    public async Task<IActionResult> UpdateNext([FromForm] SurveyFormQuestionGroupNextUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormQuestionGroupId));
    }

    [HttpPut("reorder")]
    public async Task<IActionResult> Reorder(SurveyFormQuestionGroupReorderCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    [HttpPut("rule")]
    public async Task<IActionResult> Rule(SurveyFormQuestionGroupRuleCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    [HttpPut("toggledisabled")]
    public override async Task<IActionResult> ToggleDisabled(DisableCommand disableCommand)
    {
        return Ok(await Mediator.Send(new SurveyFormQuestionGroupToggleDisabledCommand
        {
            Id = disableCommand.Id,
        }));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] SurveyFormQuestionGroupFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string filename, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SurveyFormQuestionGroupFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(filename),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new SurveyFormQuestionGroupDeleteCommand
        {
            Id = id,
        }));
    }

    [HttpDelete("undo/{id}/{displayOrder}")]
    public async Task<IActionResult> UndoDelete([FromRoute] int id, [FromRoute] int displayOrder)
    {
        return Ok(await Mediator.Send(new SurveyFormQuestionGroupUndoDeleteCommand
        {
            Id = id,
            DisplayOrder = displayOrder
        }));
    }

}
