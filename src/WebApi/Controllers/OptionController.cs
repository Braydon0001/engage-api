using Engage.Application.Services.Options.Queries;

namespace Engage.WebApi.Controllers;

public class OptionController : BaseController
{
    [AllowAnonymous]
    [HttpGet("type/{option}")]
    public async Task<ActionResult<List<OptionDto>>> GetByType([FromRoute] OptionsQuery query, [FromQuery] string search, [FromQuery] bool isRegional)
    {
        query.Search = search;
        query.IsRegional = isRegional;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("cascading/type/{option}")]
    public async Task<ActionResult<List<OptionDto>>> GetCascading([FromRoute] GetCascadingOptionListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("cascading/type/{option}/parentId/{parentId}")]
    public async Task<ActionResult<List<OptionDto>>> GetCascadingByParent([FromRoute] GetCascadingOptionListByParentQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("enum/type/{option}")]
    public async Task<ActionResult<List<OptionDto>>> GetEnumByType([FromRoute] GetEnumOptionListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("unassigned/type/{option}/listviewmodel")]
    public async Task<ActionResult<List<OptionDto>>> GetUnassignedListViewModelByType([FromRoute] GetUnassignedOptionListVMQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("type/{option}/id/{id}")]
    public async Task<ActionResult<OptionDto>> GetById([FromRoute] OptionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOptionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateOptionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("type/{option}/id/{id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteOptionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("disable")]
    public async Task<IActionResult> ToggleDisabled(DeleteOptionCommand command)
    {
        command.Toggle = true;

        return Ok(await Mediator.Send(command));
    }
}
