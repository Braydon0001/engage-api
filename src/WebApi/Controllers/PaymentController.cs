using Engage.Application.Services.Payments.Commands;
using Engage.Application.Services.Payments.Queries;

namespace Engage.WebApi.Controllers;

public partial class PaymentController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<PaymentSubTotalDto>>> Paginated(PaymentPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<PaymentSubTotalDto>(entities));
    }

    [HttpGet("paymentbatchid/{paymentBatchId}")]
    public async Task<ActionResult<ListResult<PaymentSubTotalDto>>> GetByBatchId([FromRoute] PaymentListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new PaymentVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("process/engageregionid/{engageregionid}/paymentstatusid/{paymentstatusid}")]
    public async Task<ActionResult<ListResult<PaymentSubTotalDto>>> GetByStatus([FromRoute] int? engageRegionId, [FromRoute] int? paymentStatusId, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetProcessPaymentsListQuery
        {
            EngageRegionId = engageRegionId,
            PaymentStatusId = paymentStatusId,
        }));
    }

    [HttpGet("process/paymentstatusids/{paymentstatusids}")]
    public async Task<ActionResult<ListResult<PaymentSubTotalDto>>> GetProcessList([FromRoute] string paymentStatusIds, CancellationToken cancellationToken)
    {
        List<int> statusIds = paymentStatusIds.Split(',').Select(int.Parse).ToList();

        return Ok(await Mediator.Send(new GetProcessPaymentsListQuery
        {
            PaymentStatusIds = statusIds,
        }, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(PaymentInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.PaymentId));
    }

    [HttpPost("batch/paymentstatus")]
    public async Task<IActionResult> BatchUpdatePaymentStatus(BatchUpdatePaymentStatusCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("batch/archive")]
    public async Task<IActionResult> BatchArchive(PaymentBatchArchiveCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("statusupdate/sendemail")]
    public async Task<IActionResult> SendStatusUpdateEmail(PaymentStatusUpdateSendEmailCommand command, CancellationToken cancellationToken)
    {
        var opStatus = await Mediator.Send(command, cancellationToken);

        return Ok(opStatus);
    }

    [HttpPost("invoicenumber")]
    public async Task<IActionResult> UpdateInvoiceNumber(PaymentInvoiceNumberUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("invoicedate")]
    public async Task<IActionResult> UpdateInvoiceDate(PaymentInvoiceDateUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(PaymentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.PaymentId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] PaymentFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file/{id}/name/{name}")]
    [HttpDelete("file/{id}/name/{name}/type/{type}")]
    public async Task<ActionResult> FileDelete([FromRoute] int id, [FromRoute] string name, [FromRoute] string type, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new PaymentFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(name),
            FileType = !string.IsNullOrWhiteSpace(type) ? HttpUtility.UrlDecode(type) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeletePaymentCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

}
