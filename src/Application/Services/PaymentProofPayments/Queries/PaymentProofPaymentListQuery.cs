namespace Engage.Application.Services.PaymentProofPayments.Queries;

public class PaymentProofPaymentListQuery : IRequest<List<PaymentProofPaymentDto>>
{

}

public record PaymentProofPaymentListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentProofPaymentListQuery, List<PaymentProofPaymentDto>>
{
    public async Task<List<PaymentProofPaymentDto>> Handle(PaymentProofPaymentListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentProofPayments.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.PaymentProofPaymentId)
                              .ProjectTo<PaymentProofPaymentDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}