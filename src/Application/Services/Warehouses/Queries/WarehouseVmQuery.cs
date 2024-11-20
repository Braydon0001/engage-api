using Engage.Application.Services.Warehouses.Models;

namespace Engage.Application.Services.Warehouses.Queries;

public class GetWarehouseViewModelQuery : IRequest<WarehouseVm>
{
    public int Id { get; set; }
}

public class GetWarehouseViewModelQueryHandler : BaseQueryHandler, IRequestHandler<GetWarehouseViewModelQuery, WarehouseVm>
{
    public GetWarehouseViewModelQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<WarehouseVm> Handle(GetWarehouseViewModelQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Warehouses.Include(e => e.DC)
                                              .SingleAsync(x => x.WarehouseId == request.Id, cancellationToken);

        return _mapper.Map<Warehouse, WarehouseVm>(entity);
    }
}
