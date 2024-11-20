using Engage.Application.Services.SurveyQuestions.Commands;
using Engage.Application.Services.SurveyQuestions.Models;
using Engage.Application.Services.SurveyQuestions.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("survey")]
public class SurveyQuestionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SurveyQuestionListItemDto>>> DtoList([FromRoute] SurveyQuestionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


    [HttpGet("[action]/{surveyid}")]
    public async Task<ActionResult<ListResult<SurveyQuestionListItemDto>>> GetAllBySurveyId([FromRoute] SurveyQuestionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyQuestionVm>> Vm([FromRoute] SurveyQuestionVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreateSurveyQuestionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateSurveyQuestionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("reorder")]
    public async Task<IActionResult> ReorderSurveyQuestions(ReorderSurveyQuestionsCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteSurveyQuestion(DeleteSurveyQuestionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("undo")]
    public async Task<IActionResult> UndoDeleteSurveyQuestion(UndoDeleteSurveyQuestionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
