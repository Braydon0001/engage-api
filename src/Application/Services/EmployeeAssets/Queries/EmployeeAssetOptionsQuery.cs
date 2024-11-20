namespace Engage.Application.Services.EmployeeAssets.Queries;

public class EmployeeAssetOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int? EmployeeId { get; set; }
}

public class EmployeeAssetOptionsHandler : IRequestHandler<EmployeeAssetOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public EmployeeAssetOptionsHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(EmployeeAssetOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeAssets.AsQueryable();

        if (request.EmployeeId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeId == request.EmployeeId);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.MobileNumber, $"%{request.Search}%"));
        }


        var entities = await queryable.OrderBy(e => e.Employee.Code)
                                      .ThenBy(e => e.MobileNumber)
                                      .Select(e => new OptionDto { Id = e.EmployeeAssetId, Name = e.MobileNumber })
                                      .Take(100)
                                      .ToListAsync(cancellationToken);

        return new List<OptionDto>(entities);
    }
}
