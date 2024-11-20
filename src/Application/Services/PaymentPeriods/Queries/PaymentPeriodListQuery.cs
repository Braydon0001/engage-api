namespace Engage.Application.Services.PaymentPeriods.Queries;

public class PaymentPeriodListQuery : IRequest<List<PaymentPeriodDto>>
{

}

public record PaymentPeriodListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentPeriodListQuery, List<PaymentPeriodDto>>
{
    public async Task<List<PaymentPeriodDto>> Handle(PaymentPeriodListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentPeriods.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Number)
                              .ProjectTo<PaymentPeriodDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}