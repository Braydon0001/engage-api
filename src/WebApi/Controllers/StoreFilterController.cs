using Engage.Application.Services.StoreFilters.Commands;
using Engage.Application.Services.StoreFilters.Models;
using Engage.Application.Services.StoreFilters.Queries;

namespace Engage.WebApi.Controllers;

public class StoreFilterController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<StoreFilterDto>>> GetAll([FromQuery] StoreFiltersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreFilterVm>> GetVm([FromRoute] StoreFilterVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStoreFiltersCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormCollection form)
    {
        return Ok(await Mediator.Send(new UploadStoreFiltersCommand
        {
            File = form.Files[0]
        }));
    }

    [HttpPost("import")]
    public async Task<IActionResult> Import(ImportStoreFiltersCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new RemoveStoreFilterCommand
        {
            Id = id
        }));
    }

    [HttpDelete("filter/{filter}")]
    public async Task<IActionResult> RemoveFilters([FromRoute] RemoveStoreFiltersCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
