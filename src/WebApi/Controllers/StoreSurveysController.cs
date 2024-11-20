using Engage.Application.Services.StoreSurveys.Models;
using Engage.Application.Services.StoreSurveys.Queries;

namespace Engage.WebApi.Controllers;

[AllowAnonymous]
public class StoreSurveysController : BaseController
{
    private readonly FeatureSwitchOptions _options;

    public StoreSurveysController(IOptions<FeatureSwitchOptions> options)
    {
        _options = options.Value;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<DataResult<StoreSurveysDto>>> GetAll([FromRoute] GetStoreSurveys query, [FromQuery] int employeeId, [FromQuery] int storeId, [FromQuery] DateTime timezoneDate)
    {
        if (_options.IsNewSurveyTargeting)
        {
            return Ok(await Mediator.Send(new SurveyTargetedQuery
            {
                EmployeeId = employeeId,
                StoreId = storeId,
                TimezoneDate = timezoneDate
            }));
        }

        return Ok(await Mediator.Send(new GetTargetedSurveys2
        {
            EmployeeId = employeeId,
            StoreId = storeId,
            TimezoneDate = timezoneDate
        }));
    }

    [HttpGet("SurveysByEmployee")]
    public async Task<ActionResult<ListResult<StoreSurveyDto>>> SurveysByEmployee([FromRoute] GetSurveysByEmployeeQuery query, [FromQuery] int employeeId, [FromQuery] int storeId, [FromQuery] DateTime timezoneDate)
    {
        query.EmployeeId = employeeId;
        query.StoreId = storeId;
        query.TimezoneDate = timezoneDate;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("GetSurveyStoresByEngageRegion")]
    public async Task<ActionResult<ListResult<SelectedStoreDto>>> GetSurveyStoresByEngageRegion([FromRoute] GetSurveyStoresByEngageRegion query, [FromQuery] int surveyId, [FromQuery] int engageRegionId)
    {
        query.SurveyId = surveyId;
        query.EngageRegionId = engageRegionId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DataResult<StoreSurveyDto>>> GetById([FromRoute] GetStoreSurveyQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
