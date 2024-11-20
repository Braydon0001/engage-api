namespace Engage.Application.Services.OrderTemplateProducts.Queries;

public class OrderTemplateProductListQuery : IRequest<List<OrderTemplateProductDto>>
{
    public int OrderTemplateId { get; set; }
}

public class OrderTemplateProductListHandler : ListQueryHandler, IRequestHandler<OrderTemplateProductListQuery, List<OrderTemplateProductDto>>
{
    public OrderTemplateProductListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OrderTemplateProductDto>> Handle(OrderTemplateProductListQuery query, CancellationToken cancellationToken)
    {
        var orderTemplateGroupIds = await _context.OrderTemplateGroups.Where(e => e.OrderTemplateId == query.OrderTemplateId)
                                                                      .Select(e => e.OrderTemplateGroupId)
                                                                      .ToListAsync(cancellationToken);

        var queryable = _context.OrderTemplateProducts.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => orderTemplateGroupIds.Contains(e.OrderTemplateGroupId));

        return await queryable.OrderBy(e => e.OrderTemplateGroupId)
                              .ProjectTo<OrderTemplateProductDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}