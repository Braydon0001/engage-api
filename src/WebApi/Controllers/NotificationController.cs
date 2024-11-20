using Engage.Application.Services.Notifications.Commands;
using Engage.Application.Services.Notifications.Models;
using Engage.Application.Services.Notifications.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("notification")]
public class NotificationController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<NotificationListDto>>> GetAll([FromRoute] GetNotificationListQuery query, [FromQuery] bool? isNational, [FromQuery] DateTime? timezoneDate)
    {
        query.IsNational = isNational;
        query.TimezoneDate = timezoneDate;
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("page")]
    public async Task<ActionResult<ListResult<NotificationListDto>>> PaginatedQuery(NotificationPaginatedQuery query, CancellationToken cancellationToken)
    {

        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpGet("GetAllByEmployee")]
    public async Task<ActionResult<ListResult<NotificationListDto>>> GetAllByEmployee([FromRoute] GetNotificationListByEmployeeQuery query, [FromQuery] int employeeId,
        [FromQuery] DateTime? timezoneDate)
    {
        query.EmployeeId = employeeId;
        query.TimezoneDate = timezoneDate;
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("national")]
    public async Task<ActionResult<ListResult<NotificationListDto>>> GetNationalNotifications([FromQuery] GetNationalNotificationsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("regional")]
    public async Task<ActionResult<ListResult<NotificationListDto>>> GetRegionalNotifications([FromQuery] GetRegionalNotificationsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NotificationVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new NotificationVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create(NotificationInsertCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [AllowAnonymous]
    [HttpPost("birthdays")]
    public async Task<IActionResult> CreateBulkBirthdays()
    {
        return Ok(await Mediator.Send(new NotificationBirthdayBulkInsertCommand { }));
    }

    [HttpPut]
    public async Task<IActionResult> Update(NotificationUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] NotificationUploadFeaturedImageCommand command)
    {
        var result = await Mediator.Send(command);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new NotificationDeleteFeaturedImageCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
