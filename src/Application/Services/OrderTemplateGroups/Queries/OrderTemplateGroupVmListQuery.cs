namespace Engage.Application.Services.OrderTemplateGroups.Queries;

public class OrderTemplateGroupVmListQuery : IRequest<List<OrderTemplateGroupVm>>
{
    public int OrderTemplateId { get; set; }
}

public class OrderTemplateGroupVmListHandler : ListQueryHandler, IRequestHandler<OrderTemplateGroupVmListQuery, List<OrderTemplateGroupVm>>
{
    public OrderTemplateGroupVmListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OrderTemplateGroupVm>> Handle(OrderTemplateGroupVmListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.OrderTemplateGroups.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.OrderTemplateId == query.OrderTemplateId);

        return await queryable.ProjectTo<OrderTemplateGroupVm>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}