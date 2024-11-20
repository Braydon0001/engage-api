using Engage.Application.Services.EmployeeStoreCheckIns.Commands;
using Engage.Application.Services.EmployeeStoreCheckIns.Models;
using Engage.Application.Services.Mobile.Stores.Queries;

namespace Engage.WebApi.Controllers.Mobile
{
    public class CheckInController : BaseMobileController
    {
        [HttpGet("FetchAll")]
        public async Task<ActionResult<ListResult<EmployeeStoreCheckInDto>>> FetchAll(
        [FromRoute] GetEmployeeStoreCheckInListQuery query,
        [FromQuery] int employeeId, [FromQuery] string search)
        {
            query.EmployeeId = employeeId;
            query.Search = search;
            return Ok(await Mediator.Send(query));
        }


        [HttpPost("CheckIn")]
        public async Task<IActionResult> Checkin(CreateEmployeeStoreCheckInCommand command) =>
            Ok(await Mediator.Send(command));


        [HttpPut("CheckOut")]
        public async Task<IActionResult> CheckOut(UpdateEmployeeStoreCheckInCommand command) =>
            Ok(await Mediator.Send(command));

        [HttpPut("CheckOutWithFallback")]
        public async Task<IActionResult> CheckOutWithFallback(UpdateEmployeeStoreCheckInWithFallbackCommand command) =>
            Ok(await Mediator.Send(command));

    }
}

