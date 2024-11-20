using Engage.Application.Services.PosSurveys.Queries;
using Engage.WebApi.Common.Authentication;

namespace Engage.WebApi.Controllers;

public partial class PosController : BaseController
{
    [AllowAnonymous]
    [HttpGet("getposupdates")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public async Task<ActionResult<ListResult<PosSurveyDto>>> GetPosUpdates(int pageSize = 100, int pageNumber = 1, DateTime? startDate = null, DateTime? endDate = null)
    {
        var entities = await Mediator.Send(new PosPaginatedSurveyAnswersQuery
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            StartDate = startDate,
            EndDate = endDate,
        });

        return Ok(new ListResult<PosSurveyDto>(entities));
    }

    [AllowAnonymous]
    [HttpGet("getposupdates2")]
    public async Task<ActionResult<ListResult<PosSurveyDto>>> GetPosUpdates2()
    {
        var entities = await Mediator.Send(new PosPaginatedSurveyAnswersQuery { StartDate = DateTime.Now.AddMonths(-7) });

        return Ok(new ListResult<PosSurveyDto>(entities));
    }
}
