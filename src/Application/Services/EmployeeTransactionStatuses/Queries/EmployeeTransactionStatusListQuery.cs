// auto-generated
namespace Engage.Application.Services.EmployeeTransactionStatuses.Queries;

public class EmployeeTransactionStatusListQuery : IRequest<List<EmployeeTransactionStatusDto>>
{

}

public class EmployeeTransactionStatusListHandler : ListQueryHandler, IRequestHandler<EmployeeTransactionStatusListQuery, List<EmployeeTransactionStatusDto>>
{
    public EmployeeTransactionStatusListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeTransactionStatusDto>> Handle(EmployeeTransactionStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeTransactionStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EmployeeTransactionStatusId)
                              .ProjectTo<EmployeeTransactionStatusDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}