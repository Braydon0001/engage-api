using Engage.Application.Services.DCDepartments.Commands;
using Engage.Application.Services.DCDepartments.Models;
using Engage.Application.Services.DCDepartments.Queries;

namespace Engage.WebApi.Controllers;

public class DCDepartmentController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<DCDepartmentDto>>> GetAll([FromRoute] DCDepartmentsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DCDepartmentVm>> GetViewModel([FromRoute] DCDepartmentVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]

    public async Task<IActionResult> Create(CreateDCDepartmentCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateDCDepartmentCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

}
