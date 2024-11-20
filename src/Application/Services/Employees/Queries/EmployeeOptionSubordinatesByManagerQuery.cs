namespace Engage.Application.Services.Employees.Queries;

public class EmployeeOptionSubordinatesByManagerQuery : IRequest<List<OptionDto>>
{
    public int EmployeeId { get; set; }
    public string Search { get; set; }
}
public class EmployeeOptionSubordinatesByManagerHandler : IRequestHandler<EmployeeOptionSubordinatesByManagerQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUserService _user;

    public EmployeeOptionSubordinatesByManagerHandler(IAppDbContext context, IMediator mediator, IUserService user)
    {
        _context = context;
        _mediator = mediator;
        _user = user;
    }
    public async Task<List<OptionDto>> Handle(EmployeeOptionSubordinatesByManagerQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _context.Users.FirstOrDefaultAsync(e => e.Email == _user.UserName, cancellationToken) ?? throw new Exception("No user found");
        var manager = await _context.Employees.FirstOrDefaultAsync(e => e.UserId == currentUser.UserId, cancellationToken) ?? throw new Exception("No employee found");

        var queryable = _context.Employees
                                .AsQueryable()
                                .AsNoTracking()
                                .Where(e => e.ManagerId == manager.EmployeeId);

        //if (!string.IsNullOrWhiteSpace(request.Search))
        //{
        //    queryable = queryable.Where(e => (EF.Functions.Like(e.FirstName, $"%{request.Search}%"))
        //                                        || (EF.Functions.Like(e.LastName, $"%{request.Search}%"))
        //                                        || (EF.Functions.Like(e.Code, $"%{request.Search}%"))
        //                                        );
        //}

        return await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.EmployeeId, Name = e.FirstName + " " + e.LastName + " - " + e.Code })
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);
    }
}