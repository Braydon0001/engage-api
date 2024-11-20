using Engage.Application.Services.PaymentNotificationStatusUsers.Commands;
using Engage.Application.Services.PaymentNotificationStatusUsers.Models;
using Engage.Application.Services.PaymentNotificationStatusUsers.Queries;

namespace Engage.WebApi.Controllers;

public class PaymentNotificationStatusUserController : BaseController
{
    [HttpGet("paymentstatusid/{paymentstatusid}/engageregionid/{engageregionid}")]
    public async Task<ActionResult<PaginatedListResult<PaymentNotificationStatusUserDto>>> Get([FromRoute] PaymentNotificationStatusUsersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<PaymentNotificationStatusUserDto>>> GetAll([FromQuery] PaymentNotificationStatusUsersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentNotificationStatusUserVm>> GetVm([FromRoute] PaymentNotificationStatusUserVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("batch")]
    public async Task<IActionResult> BatchCreatePaymentNotificationStatusUsers(BatchCreatePaymentNotificationStatusUsersCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeletePaymentNotificationStatusUserCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }
}
