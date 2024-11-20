namespace Engage.Application.Services.PaymentPeriods.Queries;

public class PaymentPeriodOptionQuery : IRequest<List<PaymentPeriodOption>>
{
    public int? PaymentYearId { get; set; }
}

public record PaymentPeriodOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentPeriodOptionQuery, List<PaymentPeriodOption>>
{
    public async Task<List<PaymentPeriodOption>> Handle(PaymentPeriodOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentPeriods.AsQueryable().AsNoTracking();

        if (query.PaymentYearId.HasValue)
        {
            queryable = queryable.Where(e => e.PaymentYearId == query.PaymentYearId.Value);
        }

        return await queryable.OrderBy(e => e.Number)
                              .ProjectTo<PaymentPeriodOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}