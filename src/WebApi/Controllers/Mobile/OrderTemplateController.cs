using Engage.Application.Services.OrderTemplateGroups.Queries;
using Engage.Application.Services.OrderTemplates.Commands;
using Engage.Application.Services.OrderTemplates.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class OrderTemplateController : BaseMobileController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<OrderTemplateDto>>> DtoList([FromQuery] OrderTemplateListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<OrderTemplateDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<OrderTemplateOption>>> OptionList([FromQuery] OrderTemplateOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderTemplateVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new OrderTemplateVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(OrderTemplateInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.OrderTemplateId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(OrderTemplateUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderTemplateId));
    }

    [HttpPut("name")]
    public async Task<IActionResult> UpdateName(OrderTemplateUpdateNameCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderTemplateId));
    }

    [HttpPut("note")]
    public async Task<IActionResult> UpdateNote(OrderTemplateUpdateNoteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderTemplateId));
    }

    [HttpPut("startdate")]
    public async Task<IActionResult> UpdateStartDate(OrderTemplateUpdateStartDateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderTemplateId));
    }

    [HttpPut("enddate")]
    public async Task<IActionResult> UpdateEndDate(OrderTemplateUpdateEndDateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderTemplateId));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] OrderTemplateUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new OrderTemplateDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }

    [HttpGet("orderTemplateGroup/ordertemplateid/{orderTemplateId}")]
    public async Task<ActionResult<ListResult<OrderTemplateGroupVm>>> VmList([FromRoute] OrderTemplateGroupVmListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<OrderTemplateGroupVm>(entities));
    }
}