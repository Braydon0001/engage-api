using Engage.Application.Services.DCProducts.Commands;
using Engage.Application.Services.DCProducts.Models;
using Engage.Application.Services.DCProducts.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("product")]
public class DCProductController : BaseController
{
    [HttpGet("variantproductid/{variantProductId}")]
    public async Task<ActionResult<ListResult<DCProductListDto>>> GetByVariantProduct([FromRoute] DCProductsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DCProductVm>> GetVm([FromRoute] DCProductVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options/project")]
    public async Task<ActionResult<List<OptionDto>>> GetDCProductOptionsByProject([FromQuery] DCProductOptionsByProjectQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options/store")]
    public async Task<ActionResult<List<OptionDto>>> GetDCProductOptionsByStore([FromQuery] DCProductOptionsByStoreQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDCProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateDCProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] DCProductUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DCProductDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
