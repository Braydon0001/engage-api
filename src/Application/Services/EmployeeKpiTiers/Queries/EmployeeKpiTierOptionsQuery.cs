using Engage.Application.Services.EmployeeKpiTiers.Models;

namespace Engage.Application.Services.EmployeeKpiTiers.Queries;

public class EmployeeKpiTierOptionsQuery : GetQuery, IRequest<List<EmployeeKpiTierOption>>
{
    public int? EmployeeKpiId { get; set; }
}

public class EmployeeKpiTierOptionsQueryHandler : IRequestHandler<EmployeeKpiTierOptionsQuery, List<EmployeeKpiTierOption>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public EmployeeKpiTierOptionsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeeKpiTierOption>> Handle(EmployeeKpiTierOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeKpiTiers.Where(e => e.Disabled == false).AsNoTracking().AsQueryable();

        if (request.EmployeeKpiId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeKpiId == request.EmployeeKpiId.Value);
        }
        var entities = await queryable.OrderBy(e => e.Name)
                                      .ProjectTo<EmployeeKpiTierOption>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return entities;
    }
}
