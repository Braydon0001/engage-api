namespace Engage.Application.Services.PaymentLines.Queries;

public class PaymentLineListQuery : IRequest<List<PaymentLineDto>>
{
    public int PaymentId { get; set; }
}

public record PaymentLineListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineListQuery, List<PaymentLineDto>>
{
    public async Task<List<PaymentLineDto>> Handle(PaymentLineListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentLines.AsQueryable().AsNoTracking();

        if (query.PaymentId > 0)
        {
            queryable = queryable.Where(e => e.PaymentId == query.PaymentId);
        }

        return await queryable.OrderBy(e => e.PaymentLineId)
                              .ProjectTo<PaymentLineDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}