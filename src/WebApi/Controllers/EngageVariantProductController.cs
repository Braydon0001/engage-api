using Engage.Application.Services.EngageVariantProducts.Commands;
using Engage.Application.Services.EngageVariantProducts.Models;
using Engage.Application.Services.EngageVariantProducts.Queries;

namespace Engage.WebApi.Controllers;

public class EngageVariantProductController : BaseController
{

    [HttpGet("masterproductid/{masterProductId}")]
    public async Task<ActionResult<ListResult<EngageVariantProductListDto>>> GetByMasterProduct([FromRoute] EngageVariantProductsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EngageVariantProductVm>> GetVm([FromRoute] EngageVariantProductVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<List<OptionDto>>> GetEngageVariantProductOptions([FromRoute] EngageVariantProductOptionsQuery query, [FromQuery] string search)
    {
        query.Search = search;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options/{distributionCenterId}")]
    public async Task<ActionResult<List<OptionDto>>> GetEngageVariantProductOptionsByDistibutionCenter([FromRoute] EngageVariantProductOptionsByDistributionCenterQuery query, [FromQuery] string search)
    {
        query.Search = search;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options/project")]
    public async Task<ActionResult<List<OptionDto>>> GetEngageVariantProductOptionsByProject([FromQuery] EngageVariantProductOptionsByProjectQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("mobile/{distributionCenterId}")]
    public async Task<ActionResult<List<OptionDto>>> GetEngageVariantProductMobileOptions([FromRoute] EngageVariantProductMobileOptionListQuery query, [FromQuery] string search)
    {
        query.Search = search;
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("producsbyemployeesubgroup/{employeeId}")]
    public async Task<ActionResult<List<EngageVariantProductMobileDto>>> GetEngageVariantProducsByEmployeeSubGroup(int employeeId)
    {
        return Ok(await Mediator.Send(new EngageVariantProductByEmployeeSubGroupQuery
        {
            EmployeeId = employeeId
        }));
    }

    [HttpGet("mastervariant/{id}")]
    public async Task<ActionResult<List<EngageVariantProductByMasterVariantDto>>> GetVariantsByMasterVariant([FromRoute] EngageVariantProductByMasterVariantQuery query, CancellationToken cancellationToken)
    {
        return Ok(new ListResult<EngageVariantProductByMasterVariantDto>(await Mediator.Send(query, cancellationToken)));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEngageVariantProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("master")]
    public async Task<IActionResult> CreateMasterVariant(EngageMasterVariantInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return Ok(new OperationStatus()
        {
            Status = true,
            RecordsAffected = 1,
            OperationId = entity.EngageMasterProductId,
            ReturnObject = entity.EngageVariantProductId
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEngageVariantProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("master")]
    public async Task<IActionResult> UpdateMasterVariant(EngageMasterVariantUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return Ok(new OperationStatus()
        {
            Status = true,
            RecordsAffected = 1,
            OperationId = entity.EngageMasterProductId,
            ReturnObject = entity.EngageVariantProductId
        });
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] EngageVariantProductUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new EngageVariantProductDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }

    [HttpPost("paginated")]
    public async Task<ActionResult<ListResult<EngageVariantProductCatalogDto>>> GetAll([FromBody] EngageVariantProductCatalogPaginatedQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("relatedproducts")]
    public async Task<ActionResult<ListResult<EngageVariantProductCatalogDto>>> GetRelatedProducts([FromBody] EngageVariantProductRelatedCatalogPaginatedQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("catalogvm/{id}")]
    public async Task<ActionResult<EngageVariantProductCatalogVm>> GetCatalogVm([FromRoute] EngageVariantProductVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
