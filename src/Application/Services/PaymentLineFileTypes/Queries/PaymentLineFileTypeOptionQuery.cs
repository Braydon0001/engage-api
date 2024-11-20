namespace Engage.Application.Services.PaymentLineFileTypes.Queries;

public class PaymentLineFileTypeOptionQuery : IRequest<List<PaymentLineFileTypeOption>>
{ 

}

public record PaymentLineFileTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineFileTypeOptionQuery, List<PaymentLineFileTypeOption>>
{
    public async Task<List<PaymentLineFileTypeOption>> Handle(PaymentLineFileTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentLineFileTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<PaymentLineFileTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}