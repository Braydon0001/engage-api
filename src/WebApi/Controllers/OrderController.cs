using Engage.Application.Services.DistributionCenters.Queries;
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EntityContacts.Models;
using Engage.Application.Services.Mobile.Orders.Queries;
using Engage.Application.Services.OrderEngageDepartments;
using Engage.Application.Services.Orders;
using Engage.Application.Services.Orders.Commands;
using Engage.Application.Services.Orders.Models;
using Engage.Application.Services.Orders.Queries;

namespace Engage.WebApi.Controllers;

public class OrderBatchEmailParams
{
    public List<int> OrderIds { get; set; }
    public List<EntityEmailOption> EmailAddresses { get; set; }
}

[Authorize("order")]
public class OrderController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<OrderListDto>>> Get([FromQuery] OrdersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


    [HttpPost("page")]
    public async Task<ActionResult<ListResult<OrderListDto>>> PaginatedQuery(OrderPaginatedQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById([FromRoute] OrderQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("vm/{id}")]
    public async Task<ActionResult<OrderVM>> Vm([FromRoute] OrderVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("vm2/{id}")]
    public async Task<ActionResult<OrderVm>> Vm2([FromRoute] OrderVmQuery2 query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("employeeregions")]
    public async Task<IActionResult> GetEmployeeEngageRegions([FromRoute] EmployeeEngageRegionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("employeedepartments")]
    public async Task<IActionResult> GetEmployeeEngageDepartments([FromRoute] GetEmployeeEngageDepartmentsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("distributioncentersbystore/{storeid}")]
    public async Task<IActionResult> GetDistributionCentersByStore([FromRoute] DistributionCentersByStoreQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("GetMobileOrderHistory/{employeeid}")]
    public async Task<IActionResult> GetMobileOrderHistory([FromRoute] GetMobileOrderHistoryQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("AddProduct")]
    public async Task<IActionResult> AddProduct(AddOrderProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("AddProducts")]
    public async Task<IActionResult> AddProducts(AddOrderProductsCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [AllowAnonymous]
    [HttpPost("AddMobileOrder")]
    public async Task<IActionResult> AddMobileOrder(OrderMobileInsertCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("report")]
    public async Task<IActionResult> OrderPreviewReport(OrderEmailVmQuery command, CancellationToken cancellationToken)
    {
        var orderDetails = await Mediator.Send(new OrderEmailVmQuery { Id = command.Id });
        var entity = await Mediator.Send(new OrderGenerateSummaryPdfCommand { Id = command.Id }, cancellationToken);

        entity.Position = 0;

        return File(entity, "application/pdf", $"Order Summary - {orderDetails.StoreName} - {orderDetails.OrderDate.ToShortDateString().Replace("/", "-")}.pdf");

    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateOrderCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("engagedepartments")]
    public async Task<IActionResult> UpdateOrderEngageDepartments(UpdateOrderEngageDepartmentsCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("orderdate")]
    public async Task<IActionResult> UpdateOrderDate(UpdateOrderDateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("deliverydate")]
    public async Task<IActionResult> UpdateOrderDeliveryDate(UpdateOrderDeliveryDateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("reference")]
    public async Task<IActionResult> UpdateOrderReference(UpdateOrderReferenceCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("status")]
    public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatusCommand command)
    {
        var UpdateOrder = await Mediator.Send(command);

        if (command.EmailAddresses != null && command.EmailAddresses.Count > 0)
        {
            var orderTemplateVm = await Mediator.Send(new OrderEmailVmQuery { Id = command.Id });

            string sendEmail;

            List<string> emails = command.EmailAddresses.Select(e => e.Value).ToList();
            if (orderTemplateVm.Email == "")
            {
                sendEmail = emails[0];
                emails.RemoveAt(0);
            }
            else
            {
                sendEmail = orderTemplateVm.Email;
            }

            var attachment = await Mediator.Send(new OrderGenerateSummaryPdfCommand { Id = command.Id });

            await Mediator.Send(new OrderSubmitEmailCommand
            {
                EmailAddress = sendEmail,
                StoreName = orderTemplateVm.StoreName,
                OrderDate = orderTemplateVm.OrderDate.ToShortDateString().Replace('/', '-'),
                OrderId = command.Id,
                CCEmails = emails,
                Attachment = attachment
            });
        }

        return Ok(UpdateOrder);
    }

    [HttpPut("email/send")]
    public async Task<IActionResult> SendOrderEmail(UpdateOrderStatusCommand command)
    {
        var orderTemplateVm = await Mediator.Send(new OrderEmailVmQuery { Id = command.Id });

        if (command.EmailAddresses.Count > 0)
        {

            var emails = command.EmailAddresses.Select(e => e.Value).ToList();
            var sendEmail = emails[0];
            command.EmailAddresses.RemoveAt(0);

            //Generate email

            var attachment = await Mediator.Send(new OrderGenerateSummaryPdfCommand { Id = command.Id });

            var emailSend = await Mediator.Send(new OrderSubmitEmailCommand
            {
                EmailAddress = sendEmail,
                StoreName = orderTemplateVm.StoreName,
                OrderDate = orderTemplateVm.OrderDate.ToShortDateString().Replace('/', '-'),
                OrderId = command.Id,
                Attachment = attachment,
                CCEmails = emails
            });
            return Ok(emailSend);
        }
        throw new Exception("Please enter an email address to send to");
    }

    [HttpPost("email/sendbatch")]
    public async Task<IActionResult> SendBatchOrderEmails(OrderBatchEmailParams param)
    {
        if (param.OrderIds.Any() == false)
        {
            throw new Exception("Please select atleast one order");
        }

        if (param.EmailAddresses.Any() == false)
        {
            throw new Exception("Please Enter atleast one Email Address");
        }

        if (param.EmailAddresses.Count > 0)
        {
            var emails = param.EmailAddresses.Select(e => e.Value).ToList();
            var sendEmail = emails[0];
            param.EmailAddresses.RemoveAt(0);

            foreach (var id in param.OrderIds)
            {
                var orderTemplateVm = await Mediator.Send(new OrderEmailVmQuery { Id = id });
                //Generate email
                var attachment = await Mediator.Send(new OrderGenerateSummaryPdfCommand { Id = id });

                var emailSend = await Mediator.Send(new OrderSubmitEmailCommand
                {
                    EmailAddress = sendEmail,
                    StoreName = orderTemplateVm.StoreName,
                    OrderDate = orderTemplateVm.OrderDate.ToShortDateString().Replace('/', '-'),
                    OrderId = id,
                    Attachment = attachment,
                    CCEmails = emails
                });
            }

            return Ok(true);
        }
        throw new Exception("Please enter an email address to send to");
    }

    [HttpPut("batchupdate/status")]
    public async Task<IActionResult> BatchUpdateOrderStatus(BatchUpdateOrderStatusCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("batchupdatecheck/status")]
    public async Task<IActionResult> CheckBatchUpdateOrderStatus(CheckBatchUpdateStatusCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("vatnumber")]
    public async Task<IActionResult> UpdateVatNumber(OrderVatNumberUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderId));
    }

    [HttpPut("accountnumber")]
    public async Task<IActionResult> UpdateAccountNumber(OrderAccountNumberUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderId));
    }

    [HttpPut("email")]
    public async Task<IActionResult> UpdateEmail(OrderEmailUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderId));
    }

    [HttpPut("contact")]
    public async Task<IActionResult> UpdateContact(OrderContactUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderId));
    }

    [HttpPut("address")]
    public async Task<IActionResult> UpdateAddress(OrderAddressUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderId));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] OrderUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new OrderDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
