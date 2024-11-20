// auto-generated
using Engage.Application.Services.WebPages.Commands;
using Engage.Application.Services.WebPages.Queries;

namespace Engage.WebApi.Controllers;

public partial class WebPageController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<WebPageDto>>> DtoList([FromQuery]WebPageListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<WebPageDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<WebPageOption>>> OptionList([FromQuery]WebPageOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WebPageVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new WebPageVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(WebPageInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.WebPageId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(WebPageUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.WebPageId));
    }


}