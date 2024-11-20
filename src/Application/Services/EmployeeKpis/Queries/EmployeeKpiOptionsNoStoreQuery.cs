namespace Engage.Application.Services.EmployeeKpis.Queries;

public class EmployeeKpiOptionsNoStoreQuery : GetQuery, IRequest<List<OptionDto>>
{
}

public class EmployeeKpiOptionsNoStoreQueryHandler : IRequestHandler<EmployeeKpiOptionsNoStoreQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public EmployeeKpiOptionsNoStoreQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(EmployeeKpiOptionsNoStoreQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EmployeeKpis.Where(e => e.Disabled == false && e.EmployeeKpiType.Name != "Store")
                                                   .OrderBy(e => e.Name)
                                                   .ToListAsync(cancellationToken);

        return entities.Select(e => new OptionDto(e.EmployeeKpiId, e.Name))
                       .ToList();
    }
}
