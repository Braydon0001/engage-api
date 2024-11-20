namespace Engage.Application.Services.EmployeeDivisions.Queries;

public class EmployeeDivisionOptionListQuery : IRequest<List<EmployeeDivisionOption>>
{
    public int? EmployeeId { get; set; }
    public string EmployeeIds { get; set; }
}

public class EmployeeDivisionOptionListHandler : ListQueryHandler, IRequestHandler<EmployeeDivisionOptionListQuery, List<EmployeeDivisionOption>>
{
    public EmployeeDivisionOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeDivisionOption>> Handle(EmployeeDivisionOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeDivisions.AsQueryable().AsNoTracking();


        if (query.EmployeeIds.IsNotNullOrWhiteSpace())
        {
            List<int> empIds = query.EmployeeIds.Split(',').Select(int.Parse).ToList();
            var divisionIds = await _context.EmployeeEmployeeDivisions.Where(e => empIds.Contains(e.EmployeeId))
                                                                      .Select(e => e.EmployeeDivisionId)
                                                                      .ToListAsync(cancellationToken);

            queryable = queryable.Where(e => divisionIds.Contains(e.EmployeeDivisionId));
        }

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EmployeeDivisionOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}