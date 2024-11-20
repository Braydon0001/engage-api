namespace Engage.Application.Services.EmployeeWorkRoles.Queries;

public class EmployeeWorkRoleOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int? EmployeeId { get; set; }
}

public class EmployeeWorkRoleOptionsHandler : IRequestHandler<EmployeeWorkRoleOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public EmployeeWorkRoleOptionsHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(EmployeeWorkRoleOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeWorkRoles.AsQueryable();

        if (request.EmployeeId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeId == request.EmployeeId);
        }

        var entities = await queryable.OrderBy(e => e.Employee.Code)
                                      .ThenBy(e => e.Title)
                                      .Select(e => new OptionDto { Id = e.EmployeeWorkRoleId, Name = e.Title })
                                      .ToListAsync(cancellationToken);

        return new List<OptionDto>(entities);
    }
}

