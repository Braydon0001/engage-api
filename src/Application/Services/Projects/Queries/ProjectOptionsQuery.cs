using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.Projects.Queries;

public class ProjectOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public bool IsRegional { get; set; }
}

public class ProjectOptionsQueryHandler : IRequestHandler<ProjectOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUserService _userService;

    public ProjectOptionsQueryHandler(IAppDbContext context, IMediator mediator, IUserService userService)
    {
        _context = context;
        _mediator = mediator;
        _userService = userService;
    }

    public async Task<List<OptionDto>> Handle(ProjectOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Projects.Where(e => EF.Property<string>(e, "Discriminator") == "Project")
                                         .AsNoTracking()
                                         .AsQueryable();

        if (request.IsRegional)
        {
            var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

            queryable = queryable.Where(e => engageRegionIds.Contains(e.EngageRegionId.Value));
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{request.Search}%"));
        }

        return await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.ProjectId, Name = e.Name })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);
    }
}

