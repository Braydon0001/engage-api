namespace Engage.Application.Services.PaymentLineFiles.Queries;

public class PaymentLineFileOptionQuery : IRequest<List<PaymentLineFileOption>>
{ 

}

public record PaymentLineFileOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineFileOptionQuery, List<PaymentLineFileOption>>
{
    public async Task<List<PaymentLineFileOption>> Handle(PaymentLineFileOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentLineFiles.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.PaymentLineFileId)
                              .ProjectTo<PaymentLineFileOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}