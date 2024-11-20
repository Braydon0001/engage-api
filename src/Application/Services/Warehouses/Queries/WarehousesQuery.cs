using Engage.Application.Services.Warehouses.Models;

namespace Engage.Application.Services.Warehouses.Queries;

public class WarehousesQuery : GetQuery, IRequest<ListResult<WarehouseDto>>
{
    public int DCId { get; set; }
}

public class WarehousesQueryHandler : BaseQueryHandler, IRequestHandler<WarehousesQuery, ListResult<WarehouseDto>>
{
    public WarehousesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<WarehouseDto>> Handle(WarehousesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Warehouses.Where(e => e.DCId == request.DCId )
                                                .OrderBy(e => e.Name)
                                                .ProjectTo<WarehouseDto>(_mapper.ConfigurationProvider)
                                                .ToListAsync(cancellationToken);

        return new ListResult<WarehouseDto>(entities);
    }
}
