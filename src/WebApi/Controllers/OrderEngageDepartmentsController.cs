using Engage.Application.Services.OrderEngageDepartments;

namespace Engage.WebApi.Controllers;

[Authorize("order")]
public class OrderEngageDepartmentsController : BaseController
{
    [AllowAnonymous]
    [HttpGet]
    [Route("employeeid/{employeeId}/{orderIdExclusion?}")]
    public async Task<IActionResult> OrderEngageDepartmentsByEmployeeId([FromRoute] OrderEngageDepartmentsByEmployeeIdQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    [Route("username/{userName}/{orderIdExclusion?}")]
    public async Task<IActionResult> OrderEngageDepartmentsByUserName([FromRoute] OrderEngageDepartmentsByUserNameQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
