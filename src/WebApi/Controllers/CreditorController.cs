using Engage.Application.Services.Creditors.Commands;
using Engage.Application.Services.Creditors.Queries;

namespace Engage.WebApi.Controllers;

public partial class CreditorController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<CreditorDto>>> Paginated(CreditorPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CreditorDto>(entities));
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<CreditorDto>>> GetNewList([FromQuery] CreditorsQuery query, CancellationToken cancellationToken)
    {
        List<int> statusIds = new() { (int)CreditorStatusId.New, (int)CreditorStatusId.RegionalApproved };
        query.CreditorStatusIds = statusIds;
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("process/creditorstatusids/{creditorstatusids}")]
    public async Task<ActionResult<ListResult<CreditorDto>>> GetProcessList([FromRoute] string creditorStatusIds, CancellationToken cancellationToken)
    {
        List<int> statusIds = creditorStatusIds.Split(',').Select(int.Parse).ToList();

        return Ok(await Mediator.Send(new CreditorsQuery
        {
            CreditorStatusIds = statusIds,
        }, cancellationToken));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CreditorOption>>> Options([FromQuery] CreditorOptionsQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreditorVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CreditorVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreditorInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CreditorId));
    }

    [HttpPost("batch/creditorstatus")]
    public async Task<IActionResult> BatchUpdateCreditorStatus(BatchUpdateCreditorStatusCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("sendemail")]
    public async Task<IActionResult> SendEmail(CreditorSendEmailCommand command, CancellationToken cancellationToken)
    {
        var opStatus = await Mediator.Send(command, cancellationToken);

        return Ok(opStatus);
    }

    [HttpPost("statusupdate/sendemail")]
    public async Task<IActionResult> SendStatusUpdateEmail(CreditorStatusUpdateSendEmailCommand command, CancellationToken cancellationToken)
    {
        var opStatus = await Mediator.Send(command, cancellationToken);

        return Ok(opStatus);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CreditorUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CreditorId));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] CreditorUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new CreditorDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
