using Engage.Application.Services.EmployeeJobTitles.Commands;
using Engage.Application.Services.EmployeeJobTitles.Models;
using Engage.Application.Services.EmployeeJobTitles.Queries;

namespace Engage.WebApi.Controllers;

public class
    EmployeeJobTitleController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeJobTitleDto>>> GetAll([FromQuery] EmployeeJobTitlesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/level/{level}")]
    public async Task<ActionResult<ListResult<EmployeeJobTitleDto>>> GetOptions([FromRoute] EmployeeJobTitleOptionsQuery query, [FromQuery] string search)
    {
        if (!string.IsNullOrWhiteSpace(search))
        {
            query.Search = search;
        }

        return Ok(await Mediator.Send(query));
    }

    [HttpGet("hierarchy")]
    public async Task<ActionResult<ListResult<EmployeeJobTitleDto>>> GetTree([FromQuery] EmployeeJobTitlesHierarchyQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeJobTitleVm>> GetVm([FromRoute] EmployeeJobTitleVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeJobTitleCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeJobTitleUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
