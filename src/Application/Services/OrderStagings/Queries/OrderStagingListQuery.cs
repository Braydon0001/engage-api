namespace Engage.Application.Services.OrderStagings.Queries;

public class OrderStagingListQuery : IRequest<List<OrderStagingDto>>
{

}

public record OrderStagingListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrderStagingListQuery, List<OrderStagingDto>>
{
    public async Task<List<OrderStagingDto>> Handle(OrderStagingListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.OrderStagings.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.OrderStagingId)
                              .ProjectTo<OrderStagingDto>(Mapper.ConfigurationProvider)
                              .OrderByDescending(e => e.Id)
                              .ToListAsync(cancellationToken);
    }
}