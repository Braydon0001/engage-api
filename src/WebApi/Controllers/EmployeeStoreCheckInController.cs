using Engage.Application.Services.EmployeeStoreCheckIns.Commands;
using Engage.Application.Services.EmployeeStoreCheckIns.Models;
using Engage.Application.Services.EmployeeStoreCheckIns.Queries;

namespace Engage.WebApi.Controllers;

public class EmployeeStoreCheckInController : BaseController
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeStoreCheckInDto>>> GetAll(
        [FromRoute] GetEmployeeStoreCheckInListQuery query,
        [FromQuery] int? employeeId,
        [FromQuery] int? storeId)
    {
        query.EmployeeId = employeeId;
        query.StoreId = storeId;
        return Ok(await Mediator.Send(query));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeStoreCheckInDto>> GetById([FromRoute] GetEmployeeStoreCheckInQuery query) =>
        Ok(await Mediator.Send(query));

    [HttpGet]
    [Route("[action]/{id?}")]
    public async Task<ActionResult<EmployeeStoreCheckInVM>> GetViewModel([FromRoute] GetEmployeeStoreCheckInViewModelQuery query) =>
        Ok(await Mediator.Send(query));

    //TODO rename to CheckIn
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeStoreCheckInCommand command) =>
        Ok(await Mediator.Send(command));

    //TODO rename to Create
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create2(CreateEmployeeStoreCheckInCommand2 command) =>
        Ok(await Mediator.Send(command));

    //TODO rename to CheckOut
    [AllowAnonymous]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeStoreCheckInCommand command) =>
        Ok(await Mediator.Send(command));

    //TODO rename to Update
    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> Update2(UpdateEmployeeStoreCheckInCommand2 command) =>
        Ok(await Mediator.Send(command));

}
