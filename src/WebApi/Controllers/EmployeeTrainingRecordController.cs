using Engage.Application.Services.EmployeeTrainingRecords.Commands;
using Engage.Application.Services.EmployeeTrainingRecords.Models;
using Engage.Application.Services.EmployeeTrainingRecords.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeTrainingRecordController : BaseController
{
    [HttpGet("employeeId/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeTrainingRecordDto>>> GetAllByEmployee([FromRoute] EmployeeTrainingRecordsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeTrainingRecordVm>> GetVm([FromRoute] EmployeeTrainingRecordVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeTrainingRecordCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeTrainingRecordCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
