namespace Engage.Application.Services.PaymentLineFileTypes.Queries;

public class PaymentLineFileTypeListQuery : IRequest<List<PaymentLineFileTypeDto>>
{

}

public record PaymentLineFileTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineFileTypeListQuery, List<PaymentLineFileTypeDto>>
{
    public async Task<List<PaymentLineFileTypeDto>> Handle(PaymentLineFileTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentLineFileTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<PaymentLineFileTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}