namespace Engage.Application.Services.EmployeeCoolerBoxes.Queries;

public class EmployeeCoolerBoxOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int? EmployeeId { get; set; }
}

public class EmployeeCoolerBoxOptionsHandler : IRequestHandler<EmployeeCoolerBoxOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public EmployeeCoolerBoxOptionsHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(EmployeeCoolerBoxOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeCoolerBoxes.AsQueryable();

        if (request.EmployeeId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeId == request.EmployeeId);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{request.Search}%"));
        }


        var entities = await queryable.OrderBy(e => e.Employee.Code)
                                      .ThenBy(e => e.Name)
                                      .Select(e => new OptionDto { Id = e.EmployeeCoolerBoxId, Name = e.Name })
                                      .Take(100)
                                      .ToListAsync(cancellationToken);

        return new List<OptionDto>(entities);
    }
}
