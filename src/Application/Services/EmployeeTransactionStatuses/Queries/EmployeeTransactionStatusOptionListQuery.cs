// auto-generated
namespace Engage.Application.Services.EmployeeTransactionStatuses.Queries;

public class EmployeeTransactionStatusOptionListQuery : IRequest<List<EmployeeTransactionStatusOption>>
{ 

}

public class EmployeeTransactionStatusOptionListHandler : ListQueryHandler, IRequestHandler<EmployeeTransactionStatusOptionListQuery, List<EmployeeTransactionStatusOption>>
{
    public EmployeeTransactionStatusOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeTransactionStatusOption>> Handle(EmployeeTransactionStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeTransactionStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeTransactionStatusId)
                              .ProjectTo<EmployeeTransactionStatusOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}