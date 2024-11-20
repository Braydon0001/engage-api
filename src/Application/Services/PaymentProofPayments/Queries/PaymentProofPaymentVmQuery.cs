namespace Engage.Application.Services.PaymentProofPayments.Queries;

public record PaymentProofPaymentVmQuery(int Id) : IRequest<PaymentProofPaymentVm>;

public record PaymentProofPaymentVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentProofPaymentVmQuery, PaymentProofPaymentVm>
{
    public async Task<PaymentProofPaymentVm> Handle(PaymentProofPaymentVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentProofPayments.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Payment)
                             .Include(e => e.PaymentProof);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.PaymentProofPaymentId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<PaymentProofPaymentVm>(entity);
    }
}