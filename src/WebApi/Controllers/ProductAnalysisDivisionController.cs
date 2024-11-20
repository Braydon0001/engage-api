using Engage.Application.Services.ProductAnalysisDivisions.Commands;
using Engage.Application.Services.ProductAnalysisDivisions.Models;
using Engage.Application.Services.ProductAnalysisDivisions.Queries;

namespace Engage.WebApi.Controllers;

public class ProductAnalysisDivisionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductAnalysisDivisionDto>>> GetAll([FromQuery] ProductAnalysisDivisionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductAnalysisDivisionVm>> GetVm([FromRoute] ProductAnalysisDivisionVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductAnalysisDivisionCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductAnalysisDivisionUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
