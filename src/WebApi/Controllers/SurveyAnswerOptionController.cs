using Engage.Application.Services.SurveyAnswerOptions.Commands;
using Engage.Application.Services.SurveyAnswerOptions.Models;
using Engage.Application.Services.SurveyAnswerOptions.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("survey")]
public class SurveyAnswerOptionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SurveyAnswerOptionListItemDto>>> DtoList([FromRoute] GetSurveyAnswerOptionListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyAnswerOptionVm>> Vm([FromRoute] GetSurveyAnswerOptionViewModelQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreateSurveyAnswerOptionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateSurveyAnswerOptionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }


}
