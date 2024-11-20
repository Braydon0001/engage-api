namespace Engage.Application.Services.PaymentProofs.Queries;

public class PaymentProofOptionQuery : IRequest<List<PaymentProofOption>>
{ 

}

public record PaymentProofOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentProofOptionQuery, List<PaymentProofOption>>
{
    public async Task<List<PaymentProofOption>> Handle(PaymentProofOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentProofs.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.PaymentProofId)
                              .ProjectTo<PaymentProofOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}