// auto-generated
namespace Engage.Application.Services.EmployeeRecurringTransactions.Queries;

public class EmployeeRecurringTransactionVmQuery : IRequest<EmployeeRecurringTransactionVm>
{
    public int Id { get; set; }
}

public class EmployeeRecurringTransactionVmHandler : VmQueryHandler, IRequestHandler<EmployeeRecurringTransactionVmQuery, EmployeeRecurringTransactionVm>
{
    public EmployeeRecurringTransactionVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeRecurringTransactionVm> Handle(EmployeeRecurringTransactionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeRecurringTransactions.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Employee)
                             .Include(e => e.EmployeeTransactionType)
                             .Include(e => e.EmployeeRecurringTransactionStatus)
                             .Include(e => e.PayrollPeriod)
                             .Include(e => e.CreditorBankAccount);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeRecurringTransactionId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeRecurringTransactionVm>(entity);
    }
}