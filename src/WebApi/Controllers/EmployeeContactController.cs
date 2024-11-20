using Engage.Application.Services.EmployeeContacts.Models;
using Engage.Application.Services.EmployeeContacts.Queries;

namespace Engage.WebApi.Controllers;

//[Authorize("employee")]
public class EmployeeContactController : BaseController
{
    [HttpGet("employeeId/{employeeid}")]
    public async Task<ActionResult<EmployeeContactVm>> GetAllByRegion([FromRoute] GetEmployeeContactsVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
