// auto-generated
using Engage.Application.Services.NPrintings.Queries;

namespace Engage.WebApi.Controllers;

public partial class NPrintingController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<NPrintingDto>>> DtoList([FromQuery] NPrintingListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<NPrintingDto>(entities));

    }
}