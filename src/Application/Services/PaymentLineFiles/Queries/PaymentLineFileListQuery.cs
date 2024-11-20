namespace Engage.Application.Services.PaymentLineFiles.Queries;

public class PaymentLineFileListQuery : IRequest<List<PaymentLineFileDto>>
{

}

public record PaymentLineFileListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineFileListQuery, List<PaymentLineFileDto>>
{
    public async Task<List<PaymentLineFileDto>> Handle(PaymentLineFileListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentLineFiles.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.PaymentLineFileId)
                              .ProjectTo<PaymentLineFileDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}