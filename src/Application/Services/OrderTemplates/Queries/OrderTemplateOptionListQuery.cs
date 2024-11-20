// auto-generated
namespace Engage.Application.Services.OrderTemplates.Queries;

public class OrderTemplateOptionListQuery : IRequest<List<OrderTemplateOption>>
{
    public int? DistributionCenterId { get; set; }
    public DateTime? TimezoneDate { get; set; }
}

public class OrderTemplateOptionListHandler : ListQueryHandler, IRequestHandler<OrderTemplateOptionListQuery, List<OrderTemplateOption>>
{
    public OrderTemplateOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OrderTemplateOption>> Handle(OrderTemplateOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.OrderTemplates.Where(e => e.Disabled == false)
                                               .AsQueryable().AsNoTracking();

        if (query.DistributionCenterId.HasValue)
        {
            queryable = queryable.Where(e => e.DistributionCenterId == query.DistributionCenterId);
        }

        if (query.TimezoneDate.HasValue)
        {
            queryable = queryable.Where(e => query.TimezoneDate >= e.StartDate && (!e.EndDate.HasValue || query.TimezoneDate <= e.EndDate));
        }

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<OrderTemplateOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}