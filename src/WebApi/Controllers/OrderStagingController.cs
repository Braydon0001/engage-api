using Engage.Application.Services.EntityContacts.Models;
using Engage.Application.Services.OrderStagings.Commands;
using Engage.Application.Services.OrderStagings.Queries;

namespace Engage.WebApi.Controllers;

public class OrderStagingBatchEmailParams
{
    public List<int> OrderStagingIds { get; set; }
    public List<EntityEmailOption> EmailAddresses { get; set; }
    public bool EmailContact { get; set; }
}
public partial class OrderStagingController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<OrderStagingDto>>> List([FromQuery] OrderStagingListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<OrderStagingDto>(entities));
    }

    [HttpPost("page")]
    public async Task<ActionResult<PaginatedListResult<OrderStagingDto>>> PaginatedQuery(OrderStagingPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderStagingVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new OrderStagingVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost("summary")]
    public async Task<IActionResult> OrderSummary(OrderStagingGeneratePdfCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return File(file, "application/pdf", "Test file.pdf");
    }

    [HttpPost("import")]
    public async Task<IActionResult> Import(OrderStagingImportCommand command)
    {
        var value = await Mediator.Send(command);

        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(OrderStagingInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.OrderStagingId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(OrderStagingUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderStagingId));
    }

    [HttpPost("email/sendbatch")]
    public async Task<IActionResult> SendBatchOrderStagingEmails(OrderStagingBatchEmailParams param)
    {
        if (param.OrderStagingIds.Any() == false)
        {
            throw new Exception("Please select atleast one order");
        }

        if (param.EmailAddresses.IsNullOrEmpty() && !param.EmailContact)
        {
            throw new Exception("Please Enter atleast one Email Address");
        }

        if (param.EmailAddresses.IsNullOrEmpty())
        {
            param.EmailAddresses = new List<EntityEmailOption>();
        }

        if (param.EmailAddresses.Count > 0 || param.EmailContact)
        {
            var emails = param.EmailAddresses.Select(e => e.Value).ToList();
            var sendEmail = "";
            if (!param.EmailContact)
            {
                sendEmail = emails[0];
            }

            foreach (var id in param.OrderStagingIds)
            {
                var orderTemplateVm = await Mediator.Send(new OrderStagingEmailVmQuery { Id = id });
                //Generate email
                var attachment = await Mediator.Send(new OrderStagingGeneratePdfCommand { Id = id });

                var emailSend = await Mediator.Send(new OrderStagingSubmitEmailCommand
                {
                    EmailAddress = param.EmailContact ? orderTemplateVm.Email : sendEmail,
                    StoreName = orderTemplateVm.StoreName,
                    OrderDate = orderTemplateVm.OrderDate,
                    OrderStagingId = id,
                    Attachment = attachment,
                    CCEmails = emails
                });
            }

            return Ok(true);
        }
        throw new Exception("Please enter an email address to send to");
    }

}
