namespace Engage.Application.Services.PaymentStatusHistories.Queries;

public record PaymentStatusHistoryVmQuery(int Id) : IRequest<PaymentStatusHistoryVm>;

public record PaymentStatusHistoryVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentStatusHistoryVmQuery, PaymentStatusHistoryVm>
{
    public async Task<PaymentStatusHistoryVm> Handle(PaymentStatusHistoryVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentStatusHistories.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Payment)
                             .Include(e => e.PaymentStatus);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.PaymentStatusHistoryId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<PaymentStatusHistoryVm>(entity);
    }
}