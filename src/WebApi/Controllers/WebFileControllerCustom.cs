using Engage.Application.Services.WebFiles.Commands;
using Engage.Application.Services.WebFiles.Queries;

namespace Engage.WebApi.Controllers;

public partial class WebFileController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<WebFileDto>>> List([FromQuery] WebFileListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WebFileVm>> Vm([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new WebFileVmQuery(id));

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult<OperationStatus>> Create(WebFileCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<ActionResult<OperationStatus>> Update(WebFileUpdateCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var result = await Mediator.Send(new WebFileDeleteCommand(id));

        return result == null ? NotFound() : Ok(result);
    }
}