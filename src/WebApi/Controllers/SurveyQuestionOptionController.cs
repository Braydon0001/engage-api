using Engage.Application.Services.SurveyQuestionOptions.Commands;
using Engage.Application.Services.SurveyQuestionOptions.Models;
using Engage.Application.Services.SurveyQuestionOptions.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("survey")]
public class SurveyQuestionOptionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SurveyQuestionOptionListItemDto>>> DtoList([FromRoute] SurveyQuestionOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyQuestionOptionVM>> Vm([FromRoute] SurveyQuestionOptionVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreateSurveyQuestionOptionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateSurveyQuestionOptionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
