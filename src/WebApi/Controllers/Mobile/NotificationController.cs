using Engage.Application.Services.Mobile.Notification.Queries;
using Engage.Application.Services.Notifications.Models;

namespace Engage.WebApi.Controllers.Mobile
{
    public class NotificationController : BaseMobileController
    {

        //[HttpGet("national")]
        //public async Task<ActionResult<ListResult<NotificationListItemDto>>> GetNationalNotifications([FromQuery] GetNationalNotificationsQuery query)
        //{
        //    return Ok(await Mediator.Send(query));
        //}

        //[HttpGet("regional")]
        //public async Task<ActionResult<ListResult<NotificationListItemDto>>> GetRegionalNotifications([FromQuery] GetRegionalNotificationsQuery query)
        //{
        //    return Ok(await Mediator.Send(query));
        //}

        [HttpGet("national")]
        public async Task<ActionResult<ListResult<NotificationListDto>>> GetNationalNotifications([FromQuery] int employeeId, [FromQuery] DateTime timezoneDate)
        {
            if (employeeId <= 0)
            {
                return BadRequest(BadIdText);
            }

            var result = await Mediator.Send(new GetMobileNationalNotificationQuery(employeeId, timezoneDate));

            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("regional")]
        public async Task<ActionResult<ListResult<NotificationListDto>>> GetRegionalNotifications([FromQuery] int employeeId, [FromQuery] DateTime timezoneDate)
        {
            if (employeeId <= 0)
            {
                return BadRequest(BadIdText);
            }

            var result = await Mediator.Send(new GetMobileRegionalNotificationQuery(employeeId, timezoneDate));

            return result == null ? NotFound() : Ok(result);
        }
    }
}
