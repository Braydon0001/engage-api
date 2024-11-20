using Engage.Application.Services.SurveyAnswers.Commands;
using Engage.Application.Services.SurveyAnswers.Models;
using Engage.Application.Services.SurveyAnswers.Queries;
using Engage.Application.Services.SurveyInstances.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("survey")]
public class SurveyAnswerController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SurveyAnswerListItemDto>>> DtoList([FromRoute] SurveyAnswersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyAnswerVM>> GetViewModel([FromRoute] SurveyAnswerVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("[action]/{surveyinstanceid}/{questionid}")]
    public async Task<ActionResult<SurveyAnswerCurrentQuestionDto>> GetCurrentQuestion([FromRoute] SurveyAnswerCurrentQuestionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("rule/{id}")]
    public async Task<ActionResult<SurveyAnswerWebQuestionDto>> RuleVm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyAnswerRuleQuestionQuery { Id = id });

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreateSurveyAnswerCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [AllowAnonymous]
    [HttpPost("rule")]
    public async Task<IActionResult> InsertRule(SurveyAnswerInsertRuleCommand command)
    {
        var entity = await Mediator.Send(command);

        var nextQuestion = await Mediator.Send(new SurveyInstanceWebRuleEngineQuery
        {
            SurveyAnswerId = entity.SurveyAnswerId,
            QuestionId = entity.SurveyQuestionId,
        });

        return Ok(new OperationStatus(entity.SurveyAnswerId, new
        {
            nextQuestionId = nextQuestion.SurveyQuestionId
        }));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateSurveyAnswerCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [AllowAnonymous]
    [HttpPut("rule")]
    public async Task<IActionResult> UpdateRule(SurveyAnswerUpdateRuleCommand command)
    {
        var entity = await Mediator.Send(command);

        var nextQuestion = await Mediator.Send(new SurveyInstanceWebRuleEngineQuery
        {
            SurveyAnswerId = entity.SurveyAnswerId,
            QuestionId = entity.SurveyQuestionId,
        });

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyAnswerId, new
        {
            nextQuestionId = nextQuestion.SurveyQuestionId
        }));
    }

    [AllowAnonymous]
    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] SurveyAnswerUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [AllowAnonymous]
    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SurveyAnswerDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
