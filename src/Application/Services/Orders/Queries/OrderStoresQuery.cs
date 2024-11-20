using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.Orders.Queries;

public class OrderStoresQuery : GetQuery, IRequest<List<OptionDto>>
{
}

public class GetOrderStoresQueryHandler : BaseQueryHandler, IRequestHandler<OrderStoresQuery, List<OptionDto>>
{
    private readonly IMediator _mediator;

    public GetOrderStoresQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<List<OptionDto>> Handle(OrderStoresQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Stores.AsQueryable();

        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);
        queryable = queryable.Where(e => engageRegionIds.Contains(e.EngageRegionId));

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{request.Search}%"));
        }

        return await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.StoreId, Name = e.Name })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);
    }
}
