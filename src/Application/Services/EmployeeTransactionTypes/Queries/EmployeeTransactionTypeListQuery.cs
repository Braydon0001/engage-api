// auto-generated
namespace Engage.Application.Services.EmployeeTransactionTypes.Queries;

public class EmployeeTransactionTypeListQuery : IRequest<List<EmployeeTransactionTypeDto>>
{

}

public class EmployeeTransactionTypeListHandler : ListQueryHandler, IRequestHandler<EmployeeTransactionTypeListQuery, List<EmployeeTransactionTypeDto>>
{
    public EmployeeTransactionTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeTransactionTypeDto>> Handle(EmployeeTransactionTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeTransactionTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EmployeeTransactionTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}