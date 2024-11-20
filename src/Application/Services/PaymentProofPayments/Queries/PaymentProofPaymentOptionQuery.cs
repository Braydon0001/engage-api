namespace Engage.Application.Services.PaymentProofPayments.Queries;

public class PaymentProofPaymentOptionQuery : IRequest<List<PaymentProofPaymentOption>>
{ 

}

public record PaymentProofPaymentOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentProofPaymentOptionQuery, List<PaymentProofPaymentOption>>
{
    public async Task<List<PaymentProofPaymentOption>> Handle(PaymentProofPaymentOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentProofPayments.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.PaymentProofPaymentId)
                              .ProjectTo<PaymentProofPaymentOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}