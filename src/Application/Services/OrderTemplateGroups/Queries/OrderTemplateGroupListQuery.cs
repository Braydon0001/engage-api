namespace Engage.Application.Services.OrderTemplateGroups.Queries;

public class OrderTemplateGroupListQuery : IRequest<List<OrderTemplateGroupDto>>
{
    public int OrderTemplateId { get; set; }
}

public class OrderTemplateGroupListHandler : ListQueryHandler, IRequestHandler<OrderTemplateGroupListQuery, List<OrderTemplateGroupDto>>
{
    public OrderTemplateGroupListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OrderTemplateGroupDto>> Handle(OrderTemplateGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.OrderTemplateGroups.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.OrderTemplateId == query.OrderTemplateId);

        return await queryable.ProjectTo<OrderTemplateGroupDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}