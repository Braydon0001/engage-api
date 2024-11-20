using Engage.Application.Services.DistributionCenters.Queries;
using Engage.Application.Services.Mobile.Database.Models;
using Engage.Application.Services.Mobile.Stores.Queries;
using Engage.Application.Services.Stores.Queries;
using GetStoreBySearchAndEmployeeRegionQuery = Engage.Application.Services.Mobile.Stores.Queries.GetStoreBySearchAndEmployeeRegionQuery;

namespace Engage.WebApi.Controllers.Mobile
{
    public class StoreController : BaseMobileController
    {
        [Route("GetStoreBySearchAndEmployeeRegion/{searchTerm}")]
        public async Task<ActionResult<List<StoreListDto>>> GetStoreBySearchAndEmployeeRegion([FromRoute] GetStoreBySearchAndEmployeeRegionQuery query,
        [FromQuery] int employeeId, [FromQuery] float? lat, [FromQuery] float? lon)
        {
            query.EmployeeId = employeeId;
            query.Lat = lat;
            query.Lon = lon;

            return Ok(await Mediator.Send(query));
        }

        [Route("GetStoreById")]
        public async Task<ActionResult<StoreListDto>> GetStoreById([FromRoute] GetStoreByIdQuery query,
        [FromQuery] int storeId)
        {
            query.StoreId = storeId;

            return Ok(await Mediator.Send(query));
        }

        [AllowAnonymous]
        [Route("GetStoreByEmployeeRegion")]
        public async Task<ActionResult<List<StoreListDto>>> GetStoreByEmployeeRegion([FromRoute] GetStoreByEmployeeRegionQuery query,
            [FromQuery] int employeeId)
        {
            query.EmployeeId = employeeId;

            return Ok(await Mediator.Send(query));
        }

        [HttpGet("GetStoresByEmployeeLocation")]
        public async Task<ActionResult<List<StoreListDto>>> GetStoresByEmployeeLocation(
            [FromQuery] float lat,
            [FromQuery] float lon,
            [FromQuery] int employeeId)
        {
            var query = new GetStoreListByLocationQuery()
            {
                Lat = lat,
                Lon = lon,
                EmployeeId = employeeId

            };

            return Ok(await Mediator.Send(query));
        }

        [HttpGet("GetDistributionCentersByStore/{storeid}")]
        public async Task<ActionResult<List<CascadingOptionDto>>> GetDistributionCentersByStore([FromRoute] DistributionCentersByStoreQuery query)
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

        [HttpGet("offline/option")]
        public async Task<ActionResult<List<OptionDto>>> GetByTypeOffline([FromQuery] StoreOptionsOfflineQuery query, CancellationToken cancellationToken)
        {

            return Ok(await Mediator.Send(query, cancellationToken));
        }

    }
}
