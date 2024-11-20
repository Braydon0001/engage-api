using Engage.Application.Services.ProductMasters.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductMasterController : BaseController
{
    [HttpGet("option")]
    public async Task<ActionResult<List<OptionDto>>> GetOptions([FromRoute] ProductMasterOptionsQuery query, string search)
    {
        query.Search = search;
        return Ok(await Mediator.Send(query));
    }
}
