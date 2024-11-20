// auto-generated
namespace Engage.Application.Services.OrderTemplateGroups.Queries;

public class OrderTemplateGroupOptionListQuery : IRequest<List<OrderTemplateGroup>>
{
    public int OrderTemplateId { get; set; }
}

public class OrderTemplateGroupOptionListHandler : ListQueryHandler, IRequestHandler<OrderTemplateGroupOptionListQuery, List<OrderTemplateGroup>>
{
    public OrderTemplateGroupOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OrderTemplateGroup>> Handle(OrderTemplateGroupOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.OrderTemplateGroups.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.OrderTemplateId == query.OrderTemplateId);

        return await queryable.ProjectTo<OrderTemplateGroup>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}