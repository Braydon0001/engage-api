namespace Engage.Application.Services.Employees.Queries;

public class EmployeeEngageRegionsQuery : IRequest<List<string>>
{
}

public class EmployeeEngageRegionsQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeEngageRegionsQuery, List<string>>
{

    private readonly IUserService _user;

    public EmployeeEngageRegionsQueryHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<List<string>> Handle(EmployeeEngageRegionsQuery query, CancellationToken cancellationToken)
    {
        return await _context.EmployeeRegions.Where(e => e.Employee.EmailAddress1 == _user.UserName)
                                              .Select(e => e.EngageRegion.Name)
                                              .ToListAsync(cancellationToken);
    }
}
