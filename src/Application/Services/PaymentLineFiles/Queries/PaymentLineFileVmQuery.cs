namespace Engage.Application.Services.PaymentLineFiles.Queries;

public record PaymentLineFileVmQuery(int Id) : IRequest<PaymentLineFileVm>;

public record PaymentLineFileVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineFileVmQuery, PaymentLineFileVm>
{
    public async Task<PaymentLineFileVm> Handle(PaymentLineFileVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentLineFiles.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.PaymentLine)
                             .Include(e => e.PaymentLineFileType);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.PaymentLineFileId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<PaymentLineFileVm>(entity);
    }
}