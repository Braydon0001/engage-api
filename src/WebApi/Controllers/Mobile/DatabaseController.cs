using Engage.Application.Services.Mobile.Database.Models;
using Engage.Application.Services.Mobile.Database.Queries;

namespace Engage.WebApi.Controllers.Mobile
{
    public class DatabaseController : BaseMobileController
    {
        [HttpGet]
        [Route("[action]")]
        public ActionResult Index()
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetStoresByUserId/{id}")]
        public async Task<ActionResult<List<StoreListDto>>> GetStoresByUserId(int id)
        {
            return Ok(await Mediator.Send(new GetStoresByUserIdQuery() { EmployeeId = id }));
        }

        [HttpGet]
        [Route("GetStoreDCsByUserId/{id}")]
        public async Task<ActionResult<List<StoreDCDto>>> GetStoreDCsByUserId(int id)
        {
            return Ok(await Mediator.Send(new GetStoresDCsByUserIdQuery() { EmployeeId = id }));
        }

        [HttpGet]
        [Route("GetDepartmentsByUserId/{id}")]
        public async Task<ActionResult<List<DepartmentDto>>> GetDepartmentsByUserId(int id)
        {
            return Ok(await Mediator.Send(new GetDepartmentsByUserIdQuery() { EmployeeId = id }));
        }

        [HttpGet]
        [Route("GetVariantProductsByDCIds")]
        public async Task<ActionResult<List<EngageVariantProductDto>>> GetVariantProductsByDCIds([FromQuery] List<int> dcids)
        {
            return Ok(await Mediator.Send(new GetVariantProductsByDCIdsQuery() { DcIds = dcids }));
        }

        [HttpGet]
        [Route("GetDcProductsByDCIds")]
        public async Task<ActionResult<List<DCProductDto>>> GetDcProductsByDCIds([FromQuery] List<int> dcids)
        {
            return Ok(await Mediator.Send(new GetDcProductsByDCIdsQuery() { DcIds = dcids }));
        }

    }
}
