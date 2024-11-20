namespace Engage.Application.Services.PaymentBatches.Queries;

public record PaymentBatchVmQuery(int Id) : IRequest<PaymentBatchVm>;

public record PaymentBatchVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentBatchVmQuery, PaymentBatchVm>
{
    public async Task<PaymentBatchVm> Handle(PaymentBatchVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentBatches.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.BatchRegions).ThenInclude(e => e.EngageRegion);

        var entity = await queryable.SingleOrDefaultAsync(e => e.PaymentBatchId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<PaymentBatchVm>(entity);
    }
}