namespace Engage.Application.Services.PaymentPeriods.Queries;

public record PaymentPeriodVmQuery(int Id) : IRequest<PaymentPeriodVm>;

public record PaymentPeriodVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentPeriodVmQuery, PaymentPeriodVm>
{
    public async Task<PaymentPeriodVm> Handle(PaymentPeriodVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentPeriods.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.PaymentYear);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.PaymentPeriodId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<PaymentPeriodVm>(entity);
    }
}