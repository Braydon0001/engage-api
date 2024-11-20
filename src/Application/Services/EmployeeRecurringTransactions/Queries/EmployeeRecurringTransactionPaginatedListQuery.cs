// auto-generated
namespace Engage.Application.Services.EmployeeRecurringTransactions.Queries;

public class EmployeeRecurringTransactionPaginatedListQuery : PaginatedQuery, IRequest<List<EmployeeRecurringTransactionDto>>
{

}

public class EmployeeRecurringTransactionPaginatedListHandler : ListQueryHandler, IRequestHandler<EmployeeRecurringTransactionPaginatedListQuery, List<EmployeeRecurringTransactionDto>>
{
    public EmployeeRecurringTransactionPaginatedListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeRecurringTransactionDto>> Handle(EmployeeRecurringTransactionPaginatedListQuery query, CancellationToken cancellationToken)
    {
        var paginationModels = CreatePaginationModels();

        var queryable = _context.EmployeeRecurringTransactions.AsQueryable().AsNoTracking();

        var entities = await queryable.Filter(query, paginationModels)
                                      .Sort(query, paginationModels)
                                      .Skip(query.StartRow)
                                      .Take(query.PageSize)
                                      .ProjectTo<EmployeeRecurringTransactionDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return entities;
    }

    private static Dictionary<string, PaginationProperty> CreatePaginationModels()
    {
        return new Dictionary<string, PaginationProperty> {

            { "id", new PaginationProperty("EmployeeRecurringTransactionId") },
            { "employeeName", new PaginationProperty("Employee.FirstName") },
            { "employeeTransactionTypeName", new PaginationProperty("EmployeeTransactionType.Name") },
            { "employeeRecurringTransactionStatusName", new PaginationProperty("EmployeeRecurringTransactionStatus.Name") },
            { "payrollPeriodName", new PaginationProperty("PayrollPeriod.Name") },
            { "creditorBankAccountName", new PaginationProperty("CreditorBankAccount.Name") }

        };
    }
}


