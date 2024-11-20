using Engage.Application.Services.ApiKeys.Commands;
using Engage.Application.Services.ApiKeys.Queries;

namespace Engage.WebApi.Controllers;

public partial class ApiKeyController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ApiKeyDto>>> List([FromQuery] ApiKeyListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ApiKeyDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiKeyVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ApiKeyVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ApiKeyInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ApiKeyId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ApiKeyUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ApiKeyId));
    }

}
