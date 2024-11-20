namespace Engage.Application.Services.PaymentProofs.Queries;

public class PaymentProofListQuery : IRequest<List<PaymentProofDto>>
{

}

public record PaymentProofListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentProofListQuery, List<PaymentProofDto>>
{
    public async Task<List<PaymentProofDto>> Handle(PaymentProofListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentProofs.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.PaymentProofId)
                              .ProjectTo<PaymentProofDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}