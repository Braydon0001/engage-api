using Engage.Application.Services.Organogram;

namespace Engage.WebApi.Controllers;

public class OrganogramController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<OrganogramTreeNodeDto>> OrganizationalTree(OrganogramQuery query, CancellationToken cancellationToken)
    {
        var tree = await Mediator.Send(query, cancellationToken);

        return Ok(tree);
    }

    [HttpPost("region")]
    public async Task<ActionResult<OrganogramTreeNodeDto>> OrganizationalRegionTree(OrganogramRegionalQuery query, CancellationToken cancellationToken)
    {
        var tree = await Mediator.Send(query, cancellationToken);

        return Ok(tree);
    }

    [HttpPost("departmentgroup")]
    public async Task<ActionResult<OrganogramTreeNodeDto>> OrganizationalDepartmentGroupTree(OrganogramDepartmentQuery query, CancellationToken cancellationToken)
    {
        var tree = await Mediator.Send(query, cancellationToken);

        return Ok(tree);
    }

    [HttpPost("departmentregion")]
    public async Task<ActionResult<OrganogramTreeNodeDto>> OrganizationalDepartmentRegionalTree(OrganogramDepartmentRegionQuery query, CancellationToken cancellationToken)
    {
        var tree = await Mediator.Send(query, cancellationToken);

        return Ok(tree);
    }

    [HttpPost("managerjobtitle")]
    public async Task<ActionResult<OrganogramTreeNodeDto>> OrganizationalManagerJobtitleTree(OrganogramManagerJobTitleQuery query, CancellationToken cancellationToken)
    {
        var tree = await Mediator.Send(query, cancellationToken);

        return Ok(tree);
    }
}
