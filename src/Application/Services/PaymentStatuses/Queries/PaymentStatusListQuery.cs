namespace Engage.Application.Services.PaymentStatuses.Queries;

public class PaymentStatusListQuery : IRequest<List<PaymentStatusDto>>
{

}

public record PaymentStatusListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentStatusListQuery, List<PaymentStatusDto>>
{
    public async Task<List<PaymentStatusDto>> Handle(PaymentStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.PaymentStatusId)
                              .ProjectTo<PaymentStatusDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}