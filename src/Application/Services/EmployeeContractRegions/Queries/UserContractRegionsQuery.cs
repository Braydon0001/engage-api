namespace Engage.Application.Services.EmployeeContractRegions.Queries;

public class EmployeeContractRegionsQuery : IRequest<List<int>>
{
}

public class EmployeeContractRegionsQueryHandler : IRequestHandler<EmployeeContractRegionsQuery, List<int>>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public EmployeeContractRegionsQueryHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<List<int>> Handle(EmployeeContractRegionsQuery request, CancellationToken cancellationToken)
    {
        if (_user.IsHostSupplier && !string.IsNullOrWhiteSpace(_user.UserName))
        {
            var userRegions = await _context.UserUserGroups.Where(e => e.User.Email.ToLower() == _user.UserName.ToLower() &&
                                                                       e.Disabled == false && e.UserGroup.EngageRegionId.HasValue)
                                                           .Select(e => e.UserGroup.EngageRegionId)
                                                           .ToListAsync(cancellationToken);

            if (userRegions.Count > 0)
            {
                return userRegions.Select(e => e.Value).Distinct().ToList();
            }

        }

        return [];
    }
}
