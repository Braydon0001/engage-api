// auto-generated
namespace Engage.Application.Services.PayrollPeriods.Queries;

public class PayrollPeriodListQuery : IRequest<List<PayrollPeriodDto>>
{
    public int? PayrollYearId { get; set; }
}

public class PayrollPeriodListHandler : ListQueryHandler, IRequestHandler<PayrollPeriodListQuery, List<PayrollPeriodDto>>
{
    public PayrollPeriodListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<PayrollPeriodDto>> Handle(PayrollPeriodListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.PayrollPeriods.AsQueryable().AsNoTracking();

        if (query.PayrollYearId.HasValue)
        {
            queryable = queryable.Where(e => e.PayrollYearId == query.PayrollYearId.Value);
        }

        return await queryable.OrderBy(e => e.PayrollYearId)
                              .ThenBy(e => e.Number)
                              .ProjectTo<PayrollPeriodDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}