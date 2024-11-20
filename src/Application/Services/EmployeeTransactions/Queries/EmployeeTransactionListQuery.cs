using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.EmployeeTransactions.Queries;

public record EmployeeTransactionListQuery : IRequest<List<EmployeeTransactionDto>>
{
    public int? TransactionTypeId { get; set; }
    public int? TransactionTypeGroupId { get; set; }
    public int? EmployeeId { get; set; }

    public int? EngageRegionId { get; set; }
    public int? PayrollPeriodId { get; set; }
    public int? TransactionStatusId { get; set; }
}

public class CreditorBankAccountListHandler : ListQueryHandler, IRequestHandler<EmployeeTransactionListQuery, List<EmployeeTransactionDto>>
{
    private readonly IMediator _mediator;
    public CreditorBankAccountListHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<List<EmployeeTransactionDto>> Handle(EmployeeTransactionListQuery query, CancellationToken cancellationToken)
    {
        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

        var queryable = _context.EmployeeTransactions.AsQueryable()
                                                     .AsNoTracking();

        if (!engageRegionIds.Contains(7))
        {
            queryable = queryable
                                .Join(_context.EmployeeRegions.Where(c => engageRegionIds.Contains(c.EngageRegionId)),
                                      employee => employee.EmployeeId,
                                      region => region.EmployeeId,
                                      (employee, region) => employee).Distinct();
        }

        if (query.TransactionTypeGroupId != null)
        {
            queryable = queryable.Where(e => e.EmployeeTransactionType.EmployeeTransactionTypeGroupId == query.TransactionTypeGroupId.Value);
        }

        if (query.TransactionTypeId != null)
        {
            queryable = queryable.Where(e => e.EmployeeTransactionTypeId == query.TransactionTypeId.Value);
        }

        if (query.EmployeeId != null)
        {
            queryable = queryable.Where(e => e.EmployeeId == query.EmployeeId.Value);
        }

        if (query.PayrollPeriodId != null)
        {
            queryable = queryable.Where(e => e.PayrollPeriodId == query.PayrollPeriodId.Value);
        }

        if (query.TransactionStatusId != null)
        {
            queryable = queryable.Where(e => e.EmployeeTransactionStatusId.Value == query.TransactionStatusId.Value);
        }

        if (query.EngageRegionId != null)
        {
            var employeeIds = await _context.EmployeeRegions.Where(e => e.EngageRegionId == query.EngageRegionId.Value)
                                                      .Select(e => e.EmployeeId)
                                                      .ToListAsync(cancellationToken);

            queryable = queryable.Where(e => employeeIds.Contains(e.EmployeeId));
        }

        return await queryable.OrderByDescending(e => e.EmployeeTransactionId)
                              .ProjectTo<EmployeeTransactionDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}