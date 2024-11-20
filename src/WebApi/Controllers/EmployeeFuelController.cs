using Engage.Application.Services.EmployeeFuels.Commands;
using Engage.Application.Services.EmployeeFuels.Models;
using Engage.Application.Services.EmployeeFuels.Queries;

namespace Engage.WebApi.Controllers;

public class EmployeeFuelController : BaseController
{

    [HttpGet("employeeId/{employeeId}")]
    public async Task<ActionResult<ListResult<EmployeeFuelDto>>> GetByEmployee([FromRoute] EmployeeFuelsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeFuelVm>> GetVm([FromRoute] EmployeeFuelVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeFuelCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeFuelUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<IActionResult> CreateBlob([FromForm] EmployeeFuelFileUploadCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("file/{id}/filename/{fileName}")]
    public async Task<IActionResult> DeleteBlob([FromRoute] int id)
    {
        var result = await Mediator.Send(new EmployeeFuelFileDeleteCommand
        {
            Id = id

        });

        return result == null ? NotFound() : Ok(result);
    }
}
