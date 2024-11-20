// auto-generated
using Engage.Application.Services.CreditorBankAccounts.Commands;
using Engage.Application.Services.CreditorBankAccounts.Queries;

namespace Engage.WebApi.Controllers;

public partial class CreditorBankAccountController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CreditorBankAccountDto>>> DtoList([FromQuery]CreditorBankAccountListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CreditorBankAccountDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CreditorBankAccountOption>>> OptionList([FromQuery]CreditorBankAccountOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreditorBankAccountVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CreditorBankAccountVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreditorBankAccountInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CreditorBankAccountId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CreditorBankAccountUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CreditorBankAccountId));
    }


}