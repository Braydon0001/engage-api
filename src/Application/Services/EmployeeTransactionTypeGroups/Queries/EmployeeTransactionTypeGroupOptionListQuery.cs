namespace Engage.Application.Services.EmployeeTransactionTypeGroups.Queries;

public class EmployeeTransactionTypeGroupOptionListQuery : IRequest<List<EmployeeTransactionTypeGroupOption>>
{
    public int? EmployeeTransactionTypeGroupGroupId { get; set; }
}

public class EmployeeTransactionTypeGroupOptionListHandler : ListQueryHandler, IRequestHandler<EmployeeTransactionTypeGroupOptionListQuery, List<EmployeeTransactionTypeGroupOption>>
{
    public EmployeeTransactionTypeGroupOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeTransactionTypeGroupOption>> Handle(EmployeeTransactionTypeGroupOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeTransactionTypeGroups.AsQueryable().AsNoTracking();

        if (query.EmployeeTransactionTypeGroupGroupId.HasValue)
        {
            queryable = queryable.Where(x => x.EmployeeTransactionTypeGroupId == query.EmployeeTransactionTypeGroupGroupId);
        }

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EmployeeTransactionTypeGroupOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}