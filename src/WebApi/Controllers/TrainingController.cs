using Engage.Application.Services.Trainings.Commands;
using Engage.Application.Services.Trainings.Models;
using Engage.Application.Services.Trainings.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class TrainingController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<TrainingDto>>> GetAll([FromQuery] TrainingsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option")]
    public async Task<ActionResult<ListResult<OptionDto>>> GetOptions([FromRoute] TrainingOptionsQuery query, [FromQuery] string search)
    {
        query.Search = search;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingVm>> GetVm([FromRoute] TrainingVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/engageregionIds/{engageRegionIds}/")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByEmployee([FromQuery] EngageRegionTrainingOptionsQuery query, [FromRoute] string engageRegionIds)
    {
        List<int> regionIds = engageRegionIds.Split(',').Select(int.Parse).ToList();
        query.EngageRegionIds = regionIds;
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTrainingCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTrainingCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] TrainingUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new TrainingDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
