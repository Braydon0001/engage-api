namespace Engage.Application.Services.PaymentLines.Queries;

public class PaymentLineOptionQuery : IRequest<List<PaymentLineOption>>
{ 

}

public record PaymentLineOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineOptionQuery, List<PaymentLineOption>>
{
    public async Task<List<PaymentLineOption>> Handle(PaymentLineOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentLines.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.PaymentLineId)
                              .ProjectTo<PaymentLineOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}