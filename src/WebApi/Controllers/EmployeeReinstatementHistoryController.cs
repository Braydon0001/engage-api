using Engage.Application.Services.EmployeeReinstatementHistories.Models;
using Engage.Application.Services.EmployeeReinstatementHistories.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeReinstatementHistoryController : BaseController
{
    [HttpGet("employeeId/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeReinstatementHistoryDto>>> GetAllByEmployee([FromRoute] EmployeeReinstatementHistoriesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
