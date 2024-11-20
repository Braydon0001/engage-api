using Engage.Application.Services.Projects.Queries;

namespace Engage.Application.Services.ProjectStores.Queries;

public class ProjectStoreDashboardQuery : GetQuery, IRequest<List<ProjectStoreDashboardDto>>
{
    public string EmailAddress { get; set; }
}

public class ProjectStoreDashboardQueryHandler : IRequestHandler<ProjectStoreDashboardQuery, List<ProjectStoreDashboardDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public ProjectStoreDashboardQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProjectStoreDashboardDto>> Handle(ProjectStoreDashboardQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ProjectStores.AsNoTracking().AsQueryable();

        var user = await _context.Users.Where(e => e.Email.ToLower() == request.EmailAddress.ToLower()).FirstOrDefaultAsync(cancellationToken)
                ?? throw new Exception("Current user not found");

        var stakeholderIds = await _context.ProjectStakeholderUsers.Where(e => e.UserId == user.UserId
                                                                    && e.Project.ProjectStatusId != (int)ProjectStatusId.Completed)
                                                             .Select(e => e.ProjectStakeholderId)
                                                             .ToListAsync(cancellationToken);

        var projectIds = await _context.ProjectAssignees.Where(e => stakeholderIds.Contains(e.ProjectStakeholderId)).Select(e => e.ProjectId).ToListAsync(cancellationToken);

        var projects = await _context.ProjectStores.Where(e => projectIds.Contains(e.ProjectId))
                                                   .Include(e => e.ProjectSuppliers)
                                                   .ThenInclude(e => e.Supplier)
                                                   .OrderByDescending(e => e.Created)
                                                   .ProjectTo<ProjectStoreDashboardDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);

        return projects;
    }
}

