// auto-generated
namespace Engage.Application.Services.OrderTemplateStatuses.Queries;

public class OrderTemplateStatusOptionListQuery : IRequest<List<OrderTemplateStatusOption>>
{ 

}

public class OrderTemplateStatusOptionListHandler : ListQueryHandler, IRequestHandler<OrderTemplateStatusOptionListQuery, List<OrderTemplateStatusOption>>
{
    public OrderTemplateStatusOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OrderTemplateStatusOption>> Handle(OrderTemplateStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.OrderTemplateStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<OrderTemplateStatusOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}