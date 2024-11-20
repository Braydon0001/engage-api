namespace Engage.Application.Services.PaymentStatuses.Queries;

public record PaymentStatusVmQuery(int Id) : IRequest<PaymentStatusVm>;

public record PaymentStatusVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentStatusVmQuery, PaymentStatusVm>
{
    public async Task<PaymentStatusVm> Handle(PaymentStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.PaymentStatusId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<PaymentStatusVm>(entity);
    }
}