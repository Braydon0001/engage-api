// auto-generated
namespace Engage.Application.Services.EmployeeRecurringTransactionStatuses.Queries;

public class EmployeeRecurringTransactionStatusOptionListQuery : IRequest<List<EmployeeRecurringTransactionStatusOption>>
{ 

}

public class EmployeeRecurringTransactionStatusOptionListHandler : ListQueryHandler, IRequestHandler<EmployeeRecurringTransactionStatusOptionListQuery, List<EmployeeRecurringTransactionStatusOption>>
{
    public EmployeeRecurringTransactionStatusOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeRecurringTransactionStatusOption>> Handle(EmployeeRecurringTransactionStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeRecurringTransactionStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeRecurringTransactionStatusId)
                              .ProjectTo<EmployeeRecurringTransactionStatusOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}