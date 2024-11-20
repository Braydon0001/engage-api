using Engage.Application.Services.EmployeeAssets.Commands;
using Engage.Application.Services.EmployeeAssets.Models;
using Engage.Application.Services.EmployeeAssets.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("Employee")]
public class EmployeeAssetController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<ListResult<EmployeeAssetDto>>> GetAll([FromQuery] EmployeeAssetsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("EmployeeId/{Employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeAssetDto>>> GetByEmployee([FromQuery] EmployeeAssetsQuery query, [FromRoute] int EmployeeId)
    {
        query.EmployeeId = EmployeeId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/mobilenumbers")]
    public async Task<ActionResult<List<OptionDto>>> GetOptions([FromQuery] EmployeeAssetOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeAssetVm>> GetVm([FromRoute] EmployeeAssetVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeAssetCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeAssetUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("reassign")]
    public async Task<IActionResult> Reassign(EmployeeAssetReassignCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("reassign/bulk")]
    public async Task<IActionResult> ReassignBulk(EmployeeAssetBulkReassignCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] EmployeeAssetUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new EmployeeAssetDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
