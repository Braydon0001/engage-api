using Engage.Application.Services.EmployeeTerminationHistories.Models;
using Engage.Application.Services.EmployeeTerminationHistories.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeTerminationHistoryController : BaseController
{
    [HttpGet("employeeId/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeTerminationHistoryDto>>> GetAllByEmployee([FromRoute] EmployeeTerminationHistoriesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
