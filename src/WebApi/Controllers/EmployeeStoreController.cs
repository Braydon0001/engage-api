using Engage.Application.Services.EmployeeStores.Commands;
using Engage.Application.Services.EmployeeStores.Models;
using Engage.Application.Services.EmployeeStores.Queries;

namespace Engage.WebApi.Controllers;

public class EmployeeStoreController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<ListResult<EmployeeStoreDto>>> GetAll([FromQuery] EmployeeStoresQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("employeeId/{employeeId}")]
    public async Task<ActionResult<PaginatedListResult<EmployeeStoreDto>>> GetByEmployee([FromQuery] PaginatedEmployeeStoresQuery query, [FromRoute] int? employeeId)
    {
        query.EmployeeId = employeeId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("storeId/{storeId}")]
    public async Task<ActionResult<PaginatedListResult<EmployeeStoreDto>>> GetByStore([FromQuery] PaginatedEmployeeStoresQuery query, [FromRoute] int? storeId)
    {
        query.StoreId = storeId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("employee/subgroup/{engagesubgroupId}")]
    public async Task<ActionResult<ListResult<EmployeeStoreDto>>> GetEmployeesBySubGroup([FromQuery] EmployeesBySubGroupQuery query, [FromRoute] int engageSubGroupId)
    {
        query.EngageSubGroupId = engageSubGroupId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeStoreVm>> GetVm([FromRoute] EmployeeStoreVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeStoreCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("batch/employee")]
    public async Task<IActionResult> CreateEmployeeStoresByEmployee(CreateEmployeeStoresByEmployeeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("batch/store")]
    public async Task<IActionResult> CreateEmployeeStoresByStore(CreateEmployeeStoresByStoreCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("copy/employee")]
    public async Task<IActionResult> CopyEmployeeStores(CopyEmployeeStoresCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("delete/store")]
    public async Task<IActionResult> DeleteStore(EmployeeStoreDeleteStoreCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("delete/department")]
    public async Task<IActionResult> DeleteEngageDepartment(EmployeeStoreDeleteEngageDepartmentCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new EmployeeStoreDeleteCommand
        {
            Id = id
        }));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeStoreCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
