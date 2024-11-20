using Engage.Application.Services.EmployeeVehicles.Commands;
using Engage.Application.Services.EmployeeVehicles.Models;
using Engage.Application.Services.EmployeeVehicles.Queries;

namespace Engage.WebApi.Controllers;

//[Authorize("Employee")]
public class EmployeeVehicleController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<ListResult<EmployeeVehicleDto>>> GetAll([FromQuery] EmployeeVehiclesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("EmployeeId/{Employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeVehicleDto>>> GetByEmployee([FromQuery] EmployeeVehiclesQuery query, [FromRoute] int EmployeeId)
    {
        query.EmployeeId = EmployeeId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/registrationnumbers")]
    public async Task<ActionResult<List<OptionDto>>> GetOptions([FromQuery] EmployeeVehicleOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/registrationnumbers/{Employeeid}")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByEmployee([FromQuery] EmployeeVehicleOptionsQuery query, [FromRoute] int EmployeeId)
    {
        query.EmployeeId = EmployeeId;
        return Ok(await Mediator.Send(query));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeVehicleVm>> GetVm([FromRoute] EmployeeVehicleVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeVehicleCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeVehicleUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("reassign")]
    public async Task<IActionResult> Reassign(EmployeeVehicleReassignCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("reassign/bulk")]
    public async Task<IActionResult> ReassignBulk(EmployeeVehicleBulkReassignCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
