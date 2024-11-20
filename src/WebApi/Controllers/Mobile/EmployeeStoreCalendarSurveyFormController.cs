using Engage.Application.Services.Mobile.SurveyForms.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public class EmployeeStoreCalendarSurveyFormController : BaseMobileController
{
    [HttpGet("employeecalendar/{employeeId}/{storeId}/{dateTime}")]
    public async Task<ActionResult<SurveyFormMobileDto>> EmployeeStoreCalendarSurveys([FromRoute] int employeeId, [FromRoute] int storeId, DateTime dateTime, CancellationToken cancellationToken)
    {
        if (employeeId <= 0 || storeId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormEmployeeStoreCalendarMobileQuery() { EmployeeId = employeeId, StoreId = storeId, DateTime = dateTime }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("checkinStoreVisit/{employeeId}/{storeId}/{dateTime}")]
    public async Task<ActionResult<SurveyFormMobileDto>> EmployeeStoreCalendarCheckinSurveys([FromRoute] int employeeId, [FromRoute] int storeId, DateTime dateTime, CancellationToken cancellationToken)
    {
        if (employeeId <= 0 || storeId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormEmployeeStoreCalendarCheckinMobileQuery() { EmployeeId = employeeId, StoreId = storeId, DateTime = dateTime }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }
}
