using Engage.Application.Services.TrainingCategories.Commands;
using Engage.Application.Services.TrainingCategories.Models;
using Engage.Application.Services.TrainingCategories.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class TrainingCategoryController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<TrainingCategoryDto>>> GetAll([FromRoute] TrainingCategoriesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingCategoryVm>> GetVm([FromRoute] TrainingCategoryVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option")]
    public async Task<ActionResult<ListResult<OptionDto>>> GetOptions([FromRoute] TrainingCategoryOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTrainingCategoryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTrainingCategoryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
