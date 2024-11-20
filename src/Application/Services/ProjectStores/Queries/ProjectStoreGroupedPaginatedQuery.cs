using Engage.Application.Services.Projects.Queries;

namespace Engage.Application.Services.ProjectStores.Queries;

public class ProjectStoreGroupedPaginatedQuery : PaginatedQuery, IRequest<Dictionary<string, List<ProjectDto>>>
{
    public bool MyIncidents { get; set; }
    public bool AssignedToMe { get; set; }
    public int? StoreId { get; set; }
    public int? ProjectPriorityId { get; set; }
    public int? ProjectTypeId { get; set; }
    public int? ProjectSubTypeId { get; set; }
    public int? ProjectCategoryId { get; set; }
    public int? ProjectSubCategoryId { get; set; }
    public int? OwnerId { get; set; }
    public string Search { get; set; }
}

public class ProjectStoreGroupedPaginatedQueryHandler : ListQueryHandler, IRequestHandler<ProjectStoreGroupedPaginatedQuery, Dictionary<string, List<ProjectDto>>>
{
    private readonly IUserService _user;
    private readonly IMediator _mediator;
    public ProjectStoreGroupedPaginatedQueryHandler(IAppDbContext context, IMapper mapper, IUserService user, IMediator mediator) : base(context, mapper)
    {
        _user = user;
        _mediator = mediator;
    }

    public async Task<Dictionary<string, List<ProjectDto>>> Handle(ProjectStoreGroupedPaginatedQuery query, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ProjectStoreGridPaginatedQuery
        {
            MyIncidents = query.MyIncidents,
            AssignedToMe = query.AssignedToMe,
            IsMobile = !query.MyIncidents && !query.AssignedToMe,
            StoreId = query.StoreId,
            ProjectPriorityId = query.ProjectPriorityId,
            ProjectTypeId = query.ProjectTypeId,
            ProjectSubTypeId = query.ProjectSubTypeId,
            ProjectCategoryId = query.ProjectCategoryId,
            ProjectSubCategoryId = query.ProjectSubCategoryId,
            UserId = query.OwnerId,
            Search = query.Search,
            PageSize = query.PageSize,
            StartRow = query.StartRow,
        }, cancellationToken);

        var groupedData = response.GroupBy(e => e.ProjectTypeName)
                              .OrderBy(e => e.Key)
                              .ToDictionary(k => k.Key, group => group.ToList());

        return groupedData;
    }
}