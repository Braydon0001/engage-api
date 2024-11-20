// auto-generated
namespace Engage.Application.Services.EmployeeTransactionTypes.Queries;

public class EmployeeTransactionTypeOptionListQuery : IRequest<List<EmployeeTransactionTypeOption>>
{
    public int? EmployeeTransactionTypeGroupId { get; set; }
}

public class EmployeeTransactionTypeOptionListHandler : ListQueryHandler, IRequestHandler<EmployeeTransactionTypeOptionListQuery, List<EmployeeTransactionTypeOption>>
{
    public EmployeeTransactionTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeTransactionTypeOption>> Handle(EmployeeTransactionTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeTransactionTypes.AsQueryable().AsNoTracking();

        if (query.EmployeeTransactionTypeGroupId.HasValue)
        {
            queryable = queryable.Where(x => x.EmployeeTransactionTypeGroupId == query.EmployeeTransactionTypeGroupId);
        }

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EmployeeTransactionTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}