namespace Engage.Application.Services.EmployeeKpis.Queries;

public class EmployeeKpiOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
}

public class EmployeeKpiOptionsQueryHandler : IRequestHandler<EmployeeKpiOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public EmployeeKpiOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(EmployeeKpiOptionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EmployeeKpis.Where(e => e.Disabled == false)
                                                   .OrderBy(e => e.Name)
                                                   .ToListAsync(cancellationToken);

        return entities.Select(e => new OptionDto(e.EmployeeKpiId, e.Name))
                       .ToList();
    }
}
