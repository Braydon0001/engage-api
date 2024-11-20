using Engage.Application.Services.ProductAnalyses.Commands;
using Engage.Application.Services.ProductAnalyses.Models;
using Engage.Application.Services.ProductAnalyses.Queries;

namespace Engage.WebApi.Controllers;

public class ProductAnalysisController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<ListResult<ProductAnalysisDto>>> GetAll([FromQuery] ProductAnalysesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductAnalysisVm>> GetVm([FromRoute] ProductAnalysisVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductAnalysisCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductAnalysisUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
