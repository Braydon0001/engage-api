using Engage.Application.Services.EmployeePopiConsents.Commands;
using Engage.Application.Services.EmployeePopiConsents.Models;
using Engage.Application.Services.EmployeePopiConsents.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeePopiConsentController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeePopiConsentDto>>> GetAllEmployeePopiConsents([FromQuery] EmployeePopiConsentsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPut("consent")]
    public async Task<IActionResult> UpdatePopiConsent([FromQuery] UpdateEmployeePopiConsentCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

}
