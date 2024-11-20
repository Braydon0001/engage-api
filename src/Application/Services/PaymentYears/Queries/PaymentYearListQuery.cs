namespace Engage.Application.Services.PaymentYears.Queries;

public class PaymentYearListQuery : IRequest<List<PaymentYearDto>>
{

}

public record PaymentYearListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentYearListQuery, List<PaymentYearDto>>
{
    public async Task<List<PaymentYearDto>> Handle(PaymentYearListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentYears.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.PaymentYearId)
                              .ProjectTo<PaymentYearDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}