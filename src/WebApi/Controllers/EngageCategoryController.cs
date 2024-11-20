using Engage.Application.Services.EngageCategories.Commands;
using Engage.Application.Services.EngageCategories.Models;
using Engage.Application.Services.EngageCategories.Queries;

namespace Engage.WebApi.Controllers;

public class EngageCategoryController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EngageCategoryDto>>> GetAll([FromQuery] EngageCategoriesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EngageCategoryVm>> GetVm([FromRoute] EngageCategoryVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<List<EngageCategoryOptionDto>>> GetOptions([FromQuery] EngageCategoryOptionQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEngageCategoryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEngageCategoryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("delete/{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        var command = new DeleteOptionCommand
        {
            Option = OptionDesc.ENGAGECATEGORIES,
            Id = id,

        };

        return Ok(await Mediator.Send(command));
    }
}
