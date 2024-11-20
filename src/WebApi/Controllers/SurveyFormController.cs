using Engage.Application.Services.SurveyForms.Commands;
using Engage.Application.Services.SurveyForms.Queries;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<SurveyFormDto>>> PaginatedQuery(SurveyFormPaginatedQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyFormVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormId));
    }

    [HttpPost("next")]
    public async Task<IActionResult> InsertNext([FromForm] SurveyFormNextInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormId));
    }

    [HttpPut("next")]
    public async Task<IActionResult> UpdateNext([FromForm] SurveyFormNextUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormId));
    }

    [HttpPut("toggleisdisabled")]
    public async Task<IActionResult> ToggleIsDisabled(SurveyFormToggleIsDisabledCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] SurveyFormFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string filename, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SurveyFormFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(filename),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
