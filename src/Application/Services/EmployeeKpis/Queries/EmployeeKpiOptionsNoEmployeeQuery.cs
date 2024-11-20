namespace Engage.Application.Services.EmployeeKpis.Queries;

public class EmployeeKpiOptionsNoEmployeeQuery : GetQuery, IRequest<List<OptionDto>>
{
}

public class EmployeeKpiOptionsNoEmployeeQueryHandler : IRequestHandler<EmployeeKpiOptionsNoEmployeeQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public EmployeeKpiOptionsNoEmployeeQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(EmployeeKpiOptionsNoEmployeeQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EmployeeKpis.Where(e => e.Disabled == false && e.EmployeeKpiType.Name != "Employee")
                                                   .OrderBy(e => e.Name)
                                                   .ToListAsync(cancellationToken);

        return entities.Select(e => new OptionDto(e.EmployeeKpiId, e.Name))
                       .ToList();
    }
}
