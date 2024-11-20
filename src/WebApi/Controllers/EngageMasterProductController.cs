using Engage.Application.Services.EngageMasterProducts.Commands;
using Engage.Application.Services.EngageMasterProducts.Models;
using Engage.Application.Services.EngageMasterProducts.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("product")]
public class EngageMasterProductController : BaseController
{


    [HttpGet("options")]
    public async Task<ActionResult<List<EngageMasterProductVm>>> GetEngageMasterProductOptions([FromRoute] EngageMasterProductOptionsQuery query, [FromQuery] string search)
    {
        query.Search = search;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("optionscode")]
    public async Task<ActionResult<List<OptionDto>>> GetEngageMasterProductOptionsWithCode([FromRoute] EngageMasterProductOptionsCodeQuery query, [FromQuery] string search)
    {
        query.Search = search;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("mastervariant/{id}")]
    public async Task<ActionResult<EngageMasterVariantVm>> GetMasterVariant([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EngageMasterVariantVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost("tree")]
    public async Task<ActionResult<ListResult<ProductListDto>>> TreeQuery(EngageMasterProductTreeQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("page")]
    public async Task<ActionResult<ListResult<EngageProductMasterVariantDto>>> PaginatedQuery(EngageMasterProductVariantPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EngageProductMasterVariantDto>(entities));
    }

    [HttpPost("relatedproducts")]
    public async Task<ActionResult<ListResult<EngageProductMasterVariantRelatedDto>>> RelatedProductsQuery(EngageMasterProductRelatedProductsQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EngageProductMasterVariantRelatedDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EngageMasterProductVm>> Vm([FromRoute] EngageMasterProductVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreateEngageMasterProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEngageMasterProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] EngageMasterProductUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new EngageMasterProductDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
