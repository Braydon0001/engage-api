namespace Engage.Application.Services.ProjectStakeholders.Queries;

public class ProjectStakeholderUserOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int ProjectId { get; set; }
}

public class ProjectStakeholderUserOptionsQueryHandler : IRequestHandler<ProjectStakeholderUserOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public ProjectStakeholderUserOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(ProjectStakeholderUserOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Users.Where(e => e.Disabled == false && e.Deleted == false)
                                      .AsQueryable();

        if (request.ProjectId != 0)
        {
            var stakeholderUserIds = await _context.ProjectStakeholderUsers.Where(e => e.ProjectId == request.ProjectId)
                                                                           .Select(e => e.UserId)
                                                                           .ToListAsync(cancellationToken);

            if (stakeholderUserIds.Count > 0)
            {
                queryable = queryable.Where(e => stakeholderUserIds.Contains(e.UserId));
            }
            else
            {
                return new List<OptionDto>();
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                queryable = queryable.Where(e => EF.Functions.Like(e.FirstName, $"%{request.Search}%")
                                                || EF.Functions.Like(e.LastName, $"%{request.Search}%")
                                                || EF.Functions.Like(e.Email, $"%{request.Search}%")
                                            );
            }

            return await queryable.Select(e => new OptionDto { Id = e.UserId, Name = e.FirstName + " " + e.LastName + " - " + e.Email })
                                  .Take(100)
                                  .OrderBy(e => e.Name)
                                  .ToListAsync(cancellationToken);
        }
        return new List<OptionDto>();
    }
}

