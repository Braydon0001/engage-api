using Engage.Application.Services.SurveyFormAnswerHistories.Commands;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormAnswerHistoryController : BaseController
{
    [HttpPut("file")]
    public async Task<IActionResult> UploadSurveyAnswerFile([FromForm] SurveyFormAnswerHistoryFileUploadCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.File == null ? NotFound() : Ok(new OperationStatus(result.Id, result.File));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string filename, [FromQuery] string fileType, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SurveyFormAnswerHistoryFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(filename),
            FileType = !string.IsNullOrWhiteSpace(fileType) ? HttpUtility.UrlDecode(fileType) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
