namespace Engage.Application.Services.Warehouses.Queries;

public class WarehouseOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int DistributionCenterId { get; set; }
}

public class WarehousesOptionsQueryHandler : BaseQueryHandler, IRequestHandler<WarehouseOptionsQuery, List<OptionDto>>
{
    public WarehousesOptionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OptionDto>> Handle(WarehouseOptionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Warehouses.Where(e => e.DCId == request.DistributionCenterId)
                                        .Select(e => new OptionDto(e.WarehouseId, e.Name))
                                        .ToListAsync(cancellationToken);
    }
}
