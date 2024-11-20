namespace Engage.Application.Services.Payments.Queries;

public record PaymentVmQuery(int Id) : IRequest<PaymentVm>;

public record PaymentVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentVmQuery, PaymentVm>
{
    public async Task<PaymentVm> Handle(PaymentVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Payments.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Creditor)
                             .Include(e => e.PaymentBatch)
                                 .ThenInclude(e => e.BatchRegions)
                                    .ThenInclude(e => e.EngageRegion)
                             .Include(e => e.PaymentStatus)
                             .Include(e => e.PaymentPeriod);

        var entity = await queryable.SingleOrDefaultAsync(e => e.PaymentId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<PaymentVm>(entity);
    }
}