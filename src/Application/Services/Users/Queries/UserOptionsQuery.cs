namespace Engage.Application.Services.Users.Queries;

public class UserOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int? SupplierId { get; set; }
    public int? ProjectId { get; set; }
}

public class UserOptionsQueryHandler : IRequestHandler<UserOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public UserOptionsQueryHandler(IAppDbContext context)
    {
        this._context = context;
    }

    public async Task<List<OptionDto>> Handle(UserOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Users.AsQueryable();

        if (request.SupplierId.HasValue)
        {
            queryable = queryable.Where(e => e.SupplierId == request.SupplierId);
        }

        if (request.ProjectId.HasValue)
        {
            var project = await _context.Projects.Where(e => e.ProjectId == request.ProjectId.Value)
                                                 .SingleOrDefaultAsync(cancellationToken);

            if (project != null)
            {
                if (project.EngageRegionId.HasValue)
                {
                    var userIds = await _context.EmployeeRegions.Where(e => e.EngageRegionId == project.EngageRegionId.Value)
                                                                .Include(e => e.Employee)
                                                                .Select(e => e.Employee.UserId)
                                                                .Distinct()
                                                                .ToListAsync(cancellationToken);

                    if (userIds.Any())
                    {
                        queryable = queryable.Where(e => userIds.Contains(e.UserId));
                    }
                }
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.FullName, $"%{request.Search}%") ||
                                             EF.Functions.Like(e.Email, $"%{request.Search}%"));
        }

        return await queryable.Where(e => e.Disabled == false)
                              .OrderBy(e => e.FullName)
                              .Select(e => new OptionDto(e.UserId, e.FullName + " - " + e.Email))
                              .ToListAsync(cancellationToken);
    }
}
