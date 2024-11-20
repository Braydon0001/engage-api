using Engage.Application.Services.ProductAnalysisGroups.Commands;
using Engage.Application.Services.ProductAnalysisGroups.Models;
using Engage.Application.Services.ProductAnalysisGroups.Queries;

namespace Engage.WebApi.Controllers;

public class ProductAnalysisGroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductAnalysisGroupDto>>> GetAll([FromQuery] ProductAnalysisGroupsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductAnalysisGroupVm>> GetVm([FromRoute] ProductAnalysisGroupVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductAnalysisGroupCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductAnalysisGroupUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
