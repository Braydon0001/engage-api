using Engage.Application.Services.EngageSubCategories.Commands;
using Engage.Application.Services.EngageSubCategories.Models;
using Engage.Application.Services.EngageSubCategories.Queries;

namespace Engage.WebApi.Controllers;

public class EngageSubCategoryController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EngageSubCategoryDto>>> GetAll([FromQuery] EngageSubCategoriesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EngageSubCategoryVm>> GetVm([FromRoute] EngageSubCategoryVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<List<EngageSubCategoryOptionDto>>> getOptions([FromRoute] EngageSubCategoryOptionQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEngageSubCategoryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEngageSubCategoryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }


    [HttpDelete("delete/{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {

        var command = new DeleteOptionCommand
        {
            Option = OptionDesc.ENGAGESUBCATEGORIES,
            Id = id,

        };

        return Ok(await Mediator.Send(command));
    }
}
