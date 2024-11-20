// auto-generated
namespace Engage.Application.Services.EmployeeTransactionTypes.Queries;

public class EmployeeTransactionTypeVmQuery : IRequest<EmployeeTransactionTypeVm>
{
    public int Id { get; set; }
}

public class EmployeeTransactionTypeVmHandler : VmQueryHandler, IRequestHandler<EmployeeTransactionTypeVmQuery, EmployeeTransactionTypeVm>
{
    public EmployeeTransactionTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeTransactionTypeVm> Handle(EmployeeTransactionTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeTransactionTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeTransactionTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeTransactionTypeVm>(entity);
    }
}