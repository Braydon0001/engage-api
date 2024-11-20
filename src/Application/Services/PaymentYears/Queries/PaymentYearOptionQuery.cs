namespace Engage.Application.Services.PaymentYears.Queries;

public class PaymentYearOptionQuery : IRequest<List<PaymentYearOption>>
{ 

}

public record PaymentYearOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentYearOptionQuery, List<PaymentYearOption>>
{
    public async Task<List<PaymentYearOption>> Handle(PaymentYearOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentYears.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.PaymentYearId)
                              .ProjectTo<PaymentYearOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}