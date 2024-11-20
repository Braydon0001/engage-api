// auto-generated
namespace Engage.Application.Services.EmployeeTransactionStatuses.Queries;

public class EmployeeTransactionStatusVmQuery : IRequest<EmployeeTransactionStatusVm>
{
    public int Id { get; set; }
}

public class EmployeeTransactionStatusVmHandler : VmQueryHandler, IRequestHandler<EmployeeTransactionStatusVmQuery, EmployeeTransactionStatusVm>
{
    public EmployeeTransactionStatusVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeTransactionStatusVm> Handle(EmployeeTransactionStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeTransactionStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeTransactionStatusId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeTransactionStatusVm>(entity);
    }
}