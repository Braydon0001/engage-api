// auto-generated
namespace Engage.Application.Services.EmployeeRecurringTransactionStatuses.Queries;

public class EmployeeRecurringTransactionStatusListQuery : IRequest<List<EmployeeRecurringTransactionStatusDto>>
{

}

public class EmployeeRecurringTransactionStatusListHandler : ListQueryHandler, IRequestHandler<EmployeeRecurringTransactionStatusListQuery, List<EmployeeRecurringTransactionStatusDto>>
{
    public EmployeeRecurringTransactionStatusListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeRecurringTransactionStatusDto>> Handle(EmployeeRecurringTransactionStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeRecurringTransactionStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeRecurringTransactionStatusId)
                              .ProjectTo<EmployeeRecurringTransactionStatusDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}