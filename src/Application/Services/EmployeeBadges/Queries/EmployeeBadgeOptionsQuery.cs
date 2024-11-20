namespace Engage.Application.Services.EmployeeBadges.Queries;

public class EmployeeBadgeOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
}

public class EmployeeBadgeOptionsQueryHandler : IRequestHandler<EmployeeBadgeOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public EmployeeBadgeOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(EmployeeBadgeOptionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EmployeeBadges.Where(e => e.Disabled == false)
                                                   .OrderBy(e => e.Name)
                                                   .ToListAsync(cancellationToken);

        return entities.Select(e => new OptionDto(e.EmployeeBadgeId, e.Name))
                       .ToList();
    }
}
