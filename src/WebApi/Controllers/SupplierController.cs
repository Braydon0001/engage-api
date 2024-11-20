using Engage.Application.Services.Suppliers.Commands;
using Engage.Application.Services.Suppliers.Models;
using Engage.Application.Services.Suppliers.Queries;

namespace Engage.WebApi.Controllers;

public class SupplierController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<SupplierListDto>>> PaginatedQuery(SupplierPaginatedQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("options/page")]
    public async Task<ActionResult<IEnumerable<SupplierOption>>> PaginatedOptionQuery(SupplierPaginatedOptionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<List<OptionDto>>> OptionsQuery([FromQuery] SupplierOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/engageSubGroupId/{engageSubGroupId}")]
    public async Task<ActionResult<List<OptionDto>>> SupplierOptionsBySubGroupy([FromRoute] SupplierOptionsBySubGroupQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("engagebrand/option/supplierId/{supplierId}")]
    public async Task<ActionResult<List<OptionDto>>> EngageBrandOptionsBySupplier([FromRoute] EngageBrandOptionsBySupplierQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierVm>> GetVm([FromRoute] SupplierVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("user")]
    public async Task<ActionResult<SupplierVm>> GetUserSupplierVm([FromRoute] UserSupplierVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("validate/code/{code}")]
    public async Task<ActionResult<bool>> ValidateSupplierCode([FromRoute] ValidateSupplierCodeQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateSupplierCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateSupplierCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] SupplierFileUploadCommand command)
    {
        var file = await Mediator.Send(command);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SupplierFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
