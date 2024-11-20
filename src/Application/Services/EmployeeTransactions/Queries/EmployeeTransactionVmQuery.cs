// auto-generated
namespace Engage.Application.Services.EmployeeTransactions.Queries;

public class EmployeeTransactionVmQuery : IRequest<EmployeeTransactionVm>
{
    public int Id { get; set; }
}

public class EmployeeTransactionVmHandler : VmQueryHandler, IRequestHandler<EmployeeTransactionVmQuery, EmployeeTransactionVm>
{
    public EmployeeTransactionVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeTransactionVm> Handle(EmployeeTransactionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeTransactions.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Employee)
                             .Include(e => e.EmployeeTransactionType)
                             .Include(e => e.EmployeeRecurringTransaction).ThenInclude(r => r.CreditorBankAccount)
                             .Include(e => e.EmployeeTransactionStatus)
                             .Include(e => e.EmployeeRecurringTransactionStatus)
                             .Include(e => e.PayrollPeriod);

        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeTransactionId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeTransactionVm>(entity);
    }
}