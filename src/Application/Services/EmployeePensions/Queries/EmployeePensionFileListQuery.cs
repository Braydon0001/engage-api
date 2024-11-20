namespace Engage.Application.Services.EmployeePensions.Queries;

public class EmployeePensionFileListQuery : IRequest<List<EmployeePensionFileDto>>
{
    public int EngageRegionId { get; set; }
    public int EmployeePensionSchemeId { get; set; }
}

public class EmployeePensionFileListHandler : ListQueryHandler, IRequestHandler<EmployeePensionFileListQuery, List<EmployeePensionFileDto>>
{
    public EmployeePensionFileListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeePensionFileDto>> Handle(EmployeePensionFileListQuery query, CancellationToken cancellationToken)
    {
        var pensionQuery = _context.EmployeePensions.Where(e => e.EmployeePensionSchemeId == query.EmployeePensionSchemeId).AsQueryable().AsNoTracking();

        var employeeIds = await _context.EmployeeRegions
                                                .Where(e => e.EngageRegionId == query.EngageRegionId)
                                                .Select(x => x.EmployeeId)
                                                .ToListAsync(cancellationToken);

        employeeIds = employeeIds.Distinct().ToList();

        pensionQuery = pensionQuery.Where(x => employeeIds.Contains(x.EmployeeId))
            .Include(e => e.Employee)
            .Include(e => e.EmployeePensionScheme);

        var entities = await pensionQuery.OrderBy(e => e.EmployeePensionId)
            .ToListAsync(cancellationToken);

        var mappedEntities = new List<EmployeePensionFileDto>();
        foreach (var entity in entities)
        {
            mappedEntities.Add(_mapper.Map<EmployeePensionFileDto>(entity));
        }

        return mappedEntities;
    }
}