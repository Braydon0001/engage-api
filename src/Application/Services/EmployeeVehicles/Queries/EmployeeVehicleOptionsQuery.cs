namespace Engage.Application.Services.EmployeeVehicles.Queries;

public class EmployeeVehicleOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int? EmployeeId { get; set; }
}

public class EmployeeVehicleOptionsHandler : IRequestHandler<EmployeeVehicleOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public EmployeeVehicleOptionsHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(EmployeeVehicleOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeVehicles.AsQueryable();

        if (request.EmployeeId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeId == request.EmployeeId);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.RegistrationNumber, $"%{request.Search}%"));
        }


        var entities = await queryable.OrderBy(e => e.Employee.Code)
                                      .ThenBy(e => e.RegistrationNumber)
                                      .Select(e => new OptionDto { Id = e.EmployeeVehicleId, Name = e.RegistrationNumber })
                                      .Take(100)
                                      .ToListAsync(cancellationToken);

        return new List<OptionDto>(entities);
    }
}
