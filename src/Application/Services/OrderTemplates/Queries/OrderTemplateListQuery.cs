// auto-generated
namespace Engage.Application.Services.OrderTemplates.Queries;

public class OrderTemplateListQuery : IRequest<List<OrderTemplateDto>>
{
    public int? DistributionCenterId { get; set; }
    public DateTime? TimezoneDate { get; set; }
}

public class OrderTemplateListHandler : ListQueryHandler, IRequestHandler<OrderTemplateListQuery, List<OrderTemplateDto>>
{
    public OrderTemplateListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OrderTemplateDto>> Handle(OrderTemplateListQuery query, CancellationToken cancellationToken)
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

        return await queryable.OrderBy(e => e.OrderTemplateId)
                              .ProjectTo<OrderTemplateDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}