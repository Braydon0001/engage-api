using Engage.Application.Services.Targetings;
using Engage.Application.Services.Targetings.Models;
using Engage.Application.Services.Targetings.Queries;

namespace Engage.WebApi.Controllers;

public class TargetingController : BaseController
{
    [HttpGet("employees/targetentity/{targetentity}/targetentityid/{targetentityid}")]
    public async Task<ActionResult<ListResult<EmployeeTargetingDto>>> GetEmployeeTargetings([FromRoute] GetEmployeeTargetingsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("stores/targetentity/{targetentity}/targetentityid/{targetentityid}")]
    public async Task<ActionResult<ListResult<StoreTargetingDto>>> GetStoreTargetings([FromRoute] GetStoreTargetingsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("stores/survey/{surveyId}")]
    public async Task<ActionResult<ListResult<StoreTargetingListDto>>> GetSurveyStoresTargeting([FromRoute] SurveyStoresTargetingQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("employees/survey/{surveyId}")]
    public async Task<ActionResult<ListResult<EmployeeTargetingListDto>>> GetSurveyEmployeesTargeting([FromRoute] SurveyEmployeesTargetingQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
