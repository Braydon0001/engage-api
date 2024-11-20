namespace Engage.Application.Services.EmployeeRegions.Queries;

public class UserRegionsQuery : IRequest<List<int>>
{
}

public class UserRegionsQueryHandler : IRequestHandler<UserRegionsQuery, List<int>>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public UserRegionsQueryHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<List<int>> Handle(UserRegionsQuery request, CancellationToken cancellationToken)
    {
        if (_user.IsHostSupplier && !string.IsNullOrWhiteSpace(_user.UserName))
        {
            var regions = await _context.Employees.Where(e => e.EmailAddress1.ToLower() == _user.UserName.ToLower() &&
                                                              e.Disabled == false)
                                                  .Join(_context.EmployeeRegions,
                                                        employee => employee.EmployeeId,
                                                        region => region.EmployeeId,
                                                        (employee, region) => region.EngageRegion)
                                                  .ToListAsync(cancellationToken);

            if (!regions.Any(e => e.IsAllRegions))
            {
                return regions.Select(e => e.Id)
                              .ToList();
            }

        }

        return await _context.EngageRegions.Where(e => e.Disabled == false)
                                           .Select(e => e.Id)
                                           .ToListAsync(cancellationToken);
    }
}
