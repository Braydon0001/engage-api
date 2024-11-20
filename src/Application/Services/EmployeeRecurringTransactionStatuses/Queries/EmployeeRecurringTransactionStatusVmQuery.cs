// auto-generated
namespace Engage.Application.Services.EmployeeRecurringTransactionStatuses.Queries;

public class EmployeeRecurringTransactionStatusVmQuery : IRequest<EmployeeRecurringTransactionStatusVm>
{
    public int Id { get; set; }
}

public class EmployeeRecurringTransactionStatusVmHandler : VmQueryHandler, IRequestHandler<EmployeeRecurringTransactionStatusVmQuery, EmployeeRecurringTransactionStatusVm>
{
    public EmployeeRecurringTransactionStatusVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeRecurringTransactionStatusVm> Handle(EmployeeRecurringTransactionStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeRecurringTransactionStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeRecurringTransactionStatusId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeRecurringTransactionStatusVm>(entity);
    }
}