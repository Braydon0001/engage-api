using Engage.Application.Services.Employees.Models;
using Engage.Application.Services.SurveyEmployees;

namespace Engage.WebApi.Controllers;

[Authorize("survey")]
public class SurveyEmployeeController : BaseController
{
    [HttpGet("survey/{surveyId}")]
    public async Task<ActionResult<ListResult<EmployeeListDto>>> GetBySurvey([FromQuery] GetQuery query, [FromRoute] int surveyId)
    {
        return Ok(await Mediator.Send(query.Merge(new SurveyEmployeesQuery
        {
            SurveyId = surveyId
        })));
    }

    [HttpPost("employees")]
    public async Task<IActionResult> CreateSurveyEmployees(CreateSurveyEmployeesCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("criteria")]
    public async Task<IActionResult> CreateSurveyEmployeesWithCriteria(CreateSurveyEmployeesWithCriteriaCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteSurveyEmployee(DeleteSurveyEmployeeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("batch/survey/{surveyId}")]
    public async Task<IActionResult> BatchDeleteSurveyEmployee([FromRoute] BatchDeleteSurveyEmployeeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }


}
