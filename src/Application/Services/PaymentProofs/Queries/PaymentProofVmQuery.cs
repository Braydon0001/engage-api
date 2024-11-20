namespace Engage.Application.Services.PaymentProofs.Queries;

public record PaymentProofVmQuery(int Id) : IRequest<PaymentProofVm>;

public record PaymentProofVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentProofVmQuery, PaymentProofVm>
{
    public async Task<PaymentProofVm> Handle(PaymentProofVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentProofs.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.PaymentProofId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<PaymentProofVm>(entity);
    }
}