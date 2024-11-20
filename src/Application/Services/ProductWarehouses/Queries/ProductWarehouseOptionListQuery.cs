// auto-generated
using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.ProductWarehouses.Queries;

public class ProductWarehouseOptionListQuery : IRequest<List<ProductWarehouseOption>>
{
    public int? ExcludeWarehouseId { get; set; }
    public bool MasterWarehouses { get; set; } = false;
    public bool IsRegional { get; set; } = false;
}

public class ProductWarehouseOptionListHandler : ListQueryHandler, IRequestHandler<ProductWarehouseOptionListQuery, List<ProductWarehouseOption>>
{
    private readonly IMediator _mediator;
    private readonly IUserService _userService;
    public ProductWarehouseOptionListHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IUserService userService) : base(context, mapper)
    {
        _mediator = mediator;
        _userService = userService;
    }

    public async Task<List<ProductWarehouseOption>> Handle(ProductWarehouseOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductWarehouses.AsQueryable().AsNoTracking();

        if (query.IsRegional)
        {
            var employee = await _context.Employees
                                         .AsNoTracking()
                                         .Where(e => e.EmailAddress1.ToLower() == _userService.UserName.ToLower())
                                         .FirstOrDefaultAsync(cancellationToken);

            if (employee != null)
            {
                var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

                var warehouseIds = await _context.ProductWarehouseRegions
                                                 .AsNoTracking()
                                                 .Where(e => engageRegionIds.Contains(e.EngageRegionId))
                                                 .Select(e => e.ProductWarehouseId)
                                                 .ToListAsync(cancellationToken);

                queryable = queryable.Where(e => warehouseIds.Contains(e.ProductWarehouseId));
            }
        }

        if (query.MasterWarehouses == false)
            queryable = queryable.Where(e => e.ParentId != null);

        if (query.ExcludeWarehouseId.HasValue)
        {
            queryable = queryable.Where(e => e.ProductWarehouseId != query.ExcludeWarehouseId);
        }

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductWarehouseOption>(_mapper.ConfigurationProvider)
                              .OrderBy(e => e.Id)
                              .ToListAsync(cancellationToken);
    }
}