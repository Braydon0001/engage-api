using Engage.Application.Services.ProjectCampaigns.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectCampaignController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<ProjectCampaignDto>>> Paginated(ProjectCampaignPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectCampaignDto>(entities));
    }

    [HttpPost("options/page")]
    public async Task<ActionResult<IEnumerable<ProjectCampaignOption>>> PaginatedOption(ProjectCampaignPaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

}
