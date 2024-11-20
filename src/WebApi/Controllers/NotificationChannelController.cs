using Engage.Application.Services.NotificationChannels.Queries;

namespace Engage.WebApi.Controllers;

public partial class NotificationChannelController : BaseController
{


    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<NotificationChannelOption>>> OptionList([FromQuery] NotificationChannelOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }


}