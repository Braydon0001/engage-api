// auto-generated
namespace Engage.Application.Services.OrderTemplateStatuses.Queries;

public class OrderTemplateStatusListQuery : IRequest<List<OrderTemplateStatusDto>>
{

}

public class OrderTemplateStatusListHandler : ListQueryHandler, IRequestHandler<OrderTemplateStatusListQuery, List<OrderTemplateStatusDto>>
{
    public OrderTemplateStatusListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OrderTemplateStatusDto>> Handle(OrderTemplateStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.OrderTemplateStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<OrderTemplateStatusDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}