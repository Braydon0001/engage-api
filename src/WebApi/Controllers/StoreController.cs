using Engage.Application.Services.Stores.Commands;
using Engage.Application.Services.Stores.Models;
using Engage.Application.Services.Stores.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("store")]
public class StoreController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<StoreListDto>>> PaginatedQuery(StorePaginatedQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("options/page")]
    public async Task<ActionResult<IEnumerable<StoreOption>>> PaginatedOptionQuery(StorePaginatedOptionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("GetStoresBySearch/{searchTerm}")]
    public async Task<ActionResult<List<StoreListDto>>> GetStoresBySearch([FromRoute] GetStoreBySearchTermQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [Route("GetStoreBySearchAndEmployeeRegion/{searchTerm}")]
    public async Task<ActionResult<List<StoreListDto>>> GetStoreBySearchAndEmployeeRegion([FromRoute] GetStoreBySearchAndEmployeeRegionQuery query,
            [FromQuery] int employeeId)
    {
        query.EmployeeId = employeeId;

        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("GetStoresByEmployeeLocation")]
    public async Task<ActionResult<List<StoreListDto>>> GetStoresByEmployeeLocation(
            [FromQuery] float lat,
            [FromQuery] float lon)
    {
        var query = new GetStoreListByLocationQuery()
        {
            Lat = lat,
            Lon = lon
        };

        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("GetStoresByEmployeeLocation2")]
    public async Task<ActionResult<List<StoreListDto>>> GetStoresByEmployeeLocation2(
        [FromRoute] GetStoreListByLocationQuery2 query,
        [FromQuery] float lat,
        [FromQuery] float lon)
    {
        query.Lat = lat;
        query.Lon = lon;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreDto>> GetById([FromRoute] GetStoreQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("vm/{id}")]
    public async Task<ActionResult<StoreVm>> GetVm([FromRoute] StoreVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("GetCallCyclesViewModel/{id?}")]
    public async Task<ActionResult<StoreCallCyclesVM>> GetCallCyclesViewModel([FromRoute] GetStoreCallCyclesViewModelQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("validate/code/{code}")]
    public async Task<ActionResult<bool>> ValidateStoreCode([FromRoute] IsStoreCodeQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("option")]
    public async Task<ActionResult<List<OptionDto>>> GetByType([FromRoute] StoreOptionsQuery query, [FromQuery] string search, bool isRegional, [FromQuery] int? storeRegionId)
    {
        query.Search = search;
        query.IsRegional = isRegional;
        query.RegionId = storeRegionId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/{id}")]
    public async Task<ActionResult<OptionDto>> GetOptionVm([FromQuery] StoreOptionVmQuery query, [FromRoute] int id, CancellationToken cancellationToken)
    {
        query.Id = id;
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("option/project")]
    public async Task<ActionResult<List<OptionDto>>> GetByProject([FromQuery] StoreOptionsByProjectQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("optionsbyregion")]
    public async Task<ActionResult<List<OptionDto>>> GetRegionalOptions([FromRoute] StoreOptionsQuery query, [FromQuery] string search)
    {
        query.Search = search;
        query.IsRegional = true;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/claimId/{claimId}/")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByEmployee([FromQuery] ClaimStoreOptionsQuery query, [FromRoute] int claimId)
    {
        query.ClaimId = claimId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/voucherId/{voucherid}/")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByEmployee([FromQuery] VoucherStoreOptionsQuery query, [FromRoute] int voucherId)
    {
        query.VoucherId = voucherId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("legal/{id}")]
    public async Task<ActionResult<StoreLegalVm>> GetLegalVmById([FromRoute] StoreLegalVmQuery query)
    {
        if (query.Id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(query);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStoreCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateStoreCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("legal")]
    public async Task<IActionResult> UpdateLegal(StoreLegalUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    public record UpdateStoreConceptIdsRecord(int Id, List<int> StoreConceptIds);

    [HttpPut("storeconceptids")]
    public async Task<IActionResult> UpdateStoreConceptIds(UpdateStoreConceptIdsRecord storeConceptIds)
    {
        return Ok(await Mediator.Send(new BatchAssignCommand(AssignDesc.CONCEPT_LEVEL_STORE, storeConceptIds.Id, storeConceptIds.StoreConceptIds)));
    }

    [HttpGet("substore/storeid/{storeId}")]
    public async Task<ActionResult<ListResult<StoreListDto>>> GetSubStores([FromQuery] GetQuery getQuery, [FromRoute] int storeId)
    {
        var query = getQuery.Merge(new StoresQuery { ParentStoreId = storeId });
        return Ok(await Mediator.Send(query));
    }

    [HttpPut("substore/add")]
    public async Task<IActionResult> AddSubStore(SubStoreAddCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("substore/remove")]
    public async Task<IActionResult> RemoveSubStore(SubStoreRemoveCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
