using Engage.Application.Services.EngageDepartments.Commands;
using Engage.Application.Services.EngageDepartments.Models;
using Engage.Application.Services.EngageDepartments.Queries;

namespace Engage.WebApi.Controllers;

public class EngageDepartmentController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EngageDepartmentDto>>> GetAll([FromQuery] EngageDepartmentsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("hierarchy")]
    public async Task<ActionResult<ListResult<EngageDepartmentDto>>> GetHierarchy([FromQuery] EngageDepartmentsHierarchyQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<List<OptionDto>>> GetListQuery([FromQuery] EngageDepartmentsListQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EngageDepartmentVm>> GetVm([FromRoute] EngageDepartmentVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EngageDepartmentCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EngageDepartmentUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
