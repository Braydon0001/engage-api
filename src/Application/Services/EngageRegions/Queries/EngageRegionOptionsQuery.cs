using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.EngageRegions.Queries;

public class EngageRegionOptionsQuery : IRequest<List<OptionDto>>
{
    public bool? IsApproveClaims { get; set; }
    public bool? IsEmployeeEnageRegions { get; set; }
    public int? ProductWarehouseId { get; set; }
}

public class EngageRegionOptionsQueryHandler : IRequestHandler<EngageRegionOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUserService _user;

    public EngageRegionOptionsQueryHandler(IAppDbContext context, IMediator mediator, IUserService user)
    {
        _context = context;
        _mediator = mediator;
        _user = user;
    }

    public async Task<List<OptionDto>> Handle(EngageRegionOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EngageRegions.Where(e => e.Disabled == false && e.Deleted == false);

        if (request.ProductWarehouseId != null)
        {
            var productWarehouseIds = await _context.ProductWarehouseRegions
                                                    .AsNoTracking()
                                                    .Where(e => e.ProductWarehouseId == request.ProductWarehouseId)
                                                    .Select(e => e.EngageRegionId)
                                                    .ToListAsync(cancellationToken);

            queryable = queryable.Where(e => productWarehouseIds.Contains(e.Id));
        }

        if (request.IsApproveClaims.HasValue)
        {
            queryable = queryable.Where(e => e.IsApproveClaims == request.IsApproveClaims);
        }

        if (request.IsEmployeeEnageRegions.HasValue && !string.IsNullOrWhiteSpace(_user.UserName))
        {
            var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);
            queryable = queryable.Where(e => engageRegionIds.Contains(e.Id));
        }

        queryable = queryable.OrderBy(e => e.Name);

        return await queryable.Select(e => new OptionDto(e.Id, e.Name))
                              .ToListAsync(cancellationToken);
    }
}
