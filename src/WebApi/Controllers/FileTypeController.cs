using Engage.Application.Services.FileTypes.Commands;
using Engage.Application.Services.FileTypes.Models;
using Engage.Application.Services.FileTypes.Queries;

namespace Engage.WebApi.Controllers;

public class FileTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<FileTypeDto>>> GetList([FromRoute] FileTypesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpGet("options")]
    public async Task<ActionResult<List<FileTypeDto>>> GetOptionList([FromRoute] FileTypeOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FileTypeVm>> GetSingle([FromRoute] FileTypeVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(FileTypeCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(FileTypeUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}