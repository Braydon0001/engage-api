namespace Engage.Application.Services.PaymentLineFileTypes.Queries;

public record PaymentLineFileTypeVmQuery(int Id) : IRequest<PaymentLineFileTypeVm>;

public record PaymentLineFileTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineFileTypeVmQuery, PaymentLineFileTypeVm>
{
    public async Task<PaymentLineFileTypeVm> Handle(PaymentLineFileTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentLineFileTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.PaymentLineFileTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<PaymentLineFileTypeVm>(entity);
    }
}