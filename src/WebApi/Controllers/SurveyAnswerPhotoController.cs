using Engage.Application.Services.SurveyAnswerPhotos.Commands;
using Engage.Application.Services.SurveyAnswerPhotos.Models;
using Engage.Application.Services.SurveyAnswerPhotos.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("survey")]
public class SurveyAnswerPhotoController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SurveyAnswerPhotoListItemDto>>> DtoList([FromRoute] GetSurveyAnswerPhotoListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


    [HttpGet("survey/{surveyId}")]
    public async Task<ActionResult<PaginatedListResult<SurveyAnswerPhotoListItemDtoBySurvey>>> GetBySurveyId([FromQuery] GetQuery query, int surveyId)
    {
        return Ok(await Mediator.Send(query.Merge(new GetSurveyAnswerPhotoListQueryBySurvey
        {
            SurveyId = surveyId
        })));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyAnswerPhotoVM>> Vm([FromRoute] GetSurveyAnswerPhotoViewModelQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreateSurveyAnswerPhotoCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateSurveyAnswerPhotoCommand command)
    {
        return
        Ok(await Mediator.Send(command));
    }
}
