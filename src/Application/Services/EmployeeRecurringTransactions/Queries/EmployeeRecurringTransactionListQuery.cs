namespace Engage.Application.Services.EmployeeRecurringTransactions.Queries;

public class EmployeeRecurringTransactionListQuery : IRequest<List<EmployeeRecurringTransactionDto>>
{
    public int TransactionTypeId { get; set; }
    public int? EmployeeId { get; set; }
}

public class CreditorBankAccountListHandler : ListQueryHandler, IRequestHandler<EmployeeRecurringTransactionListQuery, List<EmployeeRecurringTransactionDto>>
{
    public CreditorBankAccountListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeRecurringTransactionDto>> Handle(EmployeeRecurringTransactionListQuery query, CancellationToken cancellationToken)
    {
        //var queryable = _context.EmployeeRecurringTransactions.Where(e => e.EmployeeId == query.EmployeeId && e.EmployeeRecurringTransactionTypeId == query.TransactionTypeId)
        //                                             .AsQueryable()
        //                                             .AsNoTracking();
        var queryable = _context.EmployeeRecurringTransactions.Where(e => e.EmployeeTransactionTypeId == query.TransactionTypeId)
                                                     .AsQueryable()
                                                     .AsNoTracking();

        if (query.EmployeeId != null)
        {
            queryable = queryable.Where(e => e.EmployeeId == query.EmployeeId.Value);
        }

        return await queryable.OrderBy(e => e.EmployeeRecurringTransactionId)
                              .ProjectTo<EmployeeRecurringTransactionDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}