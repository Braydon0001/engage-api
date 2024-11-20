using Engage.Application.Services.EmployeeWeb.Models;
using Engage.Application.Services.EmployeeWeb.Queries;
using Engage.Application.Services.Notifications.Models;
using Engage.Application.Services.Notifications.Queries;

namespace Engage.WebApi.Controllers;

public class EmployeeWebController : BaseController
{
    [HttpGet("{employeeId}")]
    public async Task<ActionResult<EmployeeWebVm>> EmployeeWeb([FromRoute] EmployeeWebQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("stores/employeeid/{employeeId}")]
    public async Task<ActionResult<EmployeeWebStoresVm>> EmployeeWebStores([FromRoute] EmployeeWebStoresQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("storeid/{storeId}/employeeid/{employeeId}")]
    public async Task<ActionResult<EmployeeWebVm>> EmployeeWebStoreQuery([FromRoute] EmployeeWebStoreQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("assets/employeeid/{employeeId}")]
    public async Task<ActionResult<EmployeeWebVm>> EmployeeWebAssets([FromRoute] EmployeeWebAssetsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("notifications/national")]
    public async Task<ActionResult<ListResult<NotificationListDto>>> NationalNotifications([FromQuery] GetNationalNotificationsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("notifications/regional")]
    public async Task<ActionResult<ListResult<NotificationListDto>>> RegionalNotifications([FromQuery] GetRegionalNotificationsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
