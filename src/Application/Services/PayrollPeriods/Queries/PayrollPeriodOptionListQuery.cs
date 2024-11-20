// auto-generated
namespace Engage.Application.Services.PayrollPeriods.Queries;

public class PayrollPeriodOptionListQuery : IRequest<List<PayrollPeriodOption>>
{
    public int? PayrollYearId { get; set; }
}

public class PayrollPeriodOptionListHandler : ListQueryHandler, IRequestHandler<PayrollPeriodOptionListQuery, List<PayrollPeriodOption>>
{
    public PayrollPeriodOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<PayrollPeriodOption>> Handle(PayrollPeriodOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.PayrollPeriods.AsQueryable().AsNoTracking();

        if (query.PayrollYearId.HasValue)
        {
            queryable = queryable.Where(e => e.PayrollYearId == query.PayrollYearId);
        }

        return await queryable.OrderBy(e => e.PayrollYearId)
                              .ThenBy(e => e.Number)
                              .ProjectTo<PayrollPeriodOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}