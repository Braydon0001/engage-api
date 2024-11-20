namespace Engage.Application.Services.PaymentStatusHistories.Queries;

public class PaymentStatusHistoryOptionQuery : IRequest<List<PaymentStatusHistoryOption>>
{ 

}

public record PaymentStatusHistoryOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentStatusHistoryOptionQuery, List<PaymentStatusHistoryOption>>
{
    public async Task<List<PaymentStatusHistoryOption>> Handle(PaymentStatusHistoryOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentStatusHistories.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.PaymentStatusHistoryId)
                              .ProjectTo<PaymentStatusHistoryOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}