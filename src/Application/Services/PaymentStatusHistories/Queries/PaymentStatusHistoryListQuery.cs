namespace Engage.Application.Services.PaymentStatusHistories.Queries;

public class PaymentStatusHistoryListQuery : IRequest<List<PaymentStatusHistoryDto>>
{

}

public record PaymentStatusHistoryListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentStatusHistoryListQuery, List<PaymentStatusHistoryDto>>
{
    public async Task<List<PaymentStatusHistoryDto>> Handle(PaymentStatusHistoryListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentStatusHistories.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.PaymentStatusHistoryId)
                              .ProjectTo<PaymentStatusHistoryDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}