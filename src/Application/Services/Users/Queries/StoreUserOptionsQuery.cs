namespace Engage.Application.Services.Users.Queries;

public class StoreUserOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int StoreId { get; set; }
}

public class StoreUserOptionsQueryHandler : IRequestHandler<StoreUserOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public StoreUserOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(StoreUserOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Users.AsQueryable();

        if (request.StoreId != 0)
        {
            var userIds = await _context.EmployeeStores.Include(e => e.Employee)
                                                       .Where(e => e.StoreId == request.StoreId)
                                                       .Select(e => e.Employee.UserId)
                                                       .ToListAsync(cancellationToken);

            if (userIds.Count > 0)
            {
                userIds = userIds.Distinct().ToList();
                queryable = queryable.Where(e => userIds.Contains(e.UserId));

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    queryable = queryable.Where(e => EF.Functions.Like(e.FirstName, $"%{request.Search}%")
                                                        || EF.Functions.Like(e.LastName, $"%{request.Search}%")
                                                        || EF.Functions.Like(e.Email, $"%{request.Search}%")
                                                      );
                }

                return await queryable.Where(e => e.Disabled == false)
                                  .Select(e => new OptionDto { Id = e.UserId, Name = e.FirstName + " " + e.LastName + " - " + e.Email })
                                  .Take(100)
                                  .OrderBy(e => e.Name)
                                  .ToListAsync(cancellationToken);
            }
        }

        return new List<OptionDto>();
    }
}