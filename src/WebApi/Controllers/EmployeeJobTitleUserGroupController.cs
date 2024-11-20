using Engage.Application.Services.EmployeeJobTitleUserGroups.Commands;
using Engage.Application.Services.EmployeeJobTitleUserGroups.Models;
using Engage.Application.Services.EmployeeJobTitleUserGroups.Queries;

namespace Engage.WebApi.Controllers;

public class EmployeeJobTitleUserGroupController : BaseController
{
    [HttpGet("employeejobtitleid/{employeejobtitleid}/usergroupid/{usergroupid}")]
    public async Task<ActionResult<PaginatedListResult<EmployeeJobTitleUserGroupDto>>> Get([FromRoute] EmployeeJobTitleUserGroupsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeJobTitleUserGroupDto>>> GetAll([FromQuery] EmployeeJobTitleUserGroupsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeJobTitleUserGroupVm>> GetVm([FromRoute] EmployeeJobTitleUserGroupVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeJobTitleUserGroupCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("batch")]
    public async Task<IActionResult> BatchCreateEmployeeJobTitleUserGroups(BatchCreateEmployeeJobTitleUserGroupsCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("groups")]
    public async Task<ActionResult<ListResult<OptionDto>>> GetGroupsForJobTitles(EmployeeJobTitleUserGroupsListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new DeleteEmployeeJobTitleUserGroupCommand
        {
            Id = id,
        }));
    }
}
