using Engage.Application.Services.Options.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public class OptionController : BaseMobileController
{
    [HttpGet("type/{option}")]
    public async Task<ActionResult<List<OptionDto>>> GetByType([FromRoute] OptionsQuery query, [FromQuery] string search, [FromQuery] bool isRegional)
    {
        query.Search = search;
        query.IsRegional = isRegional;
        return Ok(await Mediator.Send(query));
    }

  
}
