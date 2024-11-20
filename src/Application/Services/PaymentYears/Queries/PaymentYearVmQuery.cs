namespace Engage.Application.Services.PaymentYears.Queries;

public record PaymentYearVmQuery(int Id) : IRequest<PaymentYearVm>;

public record PaymentYearVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentYearVmQuery, PaymentYearVm>
{
    public async Task<PaymentYearVm> Handle(PaymentYearVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentYears.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.PaymentYearId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<PaymentYearVm>(entity);
    }
}