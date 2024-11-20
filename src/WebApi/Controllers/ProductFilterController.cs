using Engage.Application.Services.ProductFilters.Commands;
using Engage.Application.Services.ProductFilters.Models;
using Engage.Application.Services.ProductFilters.Queries;

namespace Engage.WebApi.Controllers;

public class ProductFilterController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductFilterDto>>> GetAll([FromQuery] ProductFiltersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductFilterVm>> GetVm([FromRoute] ProductFilterVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductFiltersCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormCollection form)
    {
        return Ok(await Mediator.Send(new UploadProductFiltersCommand
        {
            File = form.Files[0]
        }));
    }

    [HttpPost("import")]
    public async Task<IActionResult> Import(ImportProductFiltersCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new RemoveProductFilterCommand
        {
            Id = id
        }));
    }

    [HttpDelete("filter/{filter}")]
    public async Task<IActionResult> RemoveFilters([FromRoute] RemoveProductFiltersCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
