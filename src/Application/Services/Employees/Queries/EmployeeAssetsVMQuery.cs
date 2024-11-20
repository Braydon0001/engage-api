using Engage.Application.Services.EmployeeRegions.Queries;
using Engage.Application.Services.Employees.Models;

namespace Engage.Application.Services.Employees.Queries;

public class EmployeeAssetsVMQuery : IRequest<EmployeeAssetsVM>
{
    public int Id { get; set; }
}
public class EmployeeAssetsVMQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeAssetsVMQuery, EmployeeAssetsVM>
{
    private readonly IMediator _mediator;
    public EmployeeAssetsVMQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<EmployeeAssetsVM> Handle(EmployeeAssetsVMQuery request, CancellationToken cancellationToken)
    {
        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);
        var entity = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new Exception("Employee not found");
        }

        if (!engageRegionIds.Contains(7))
        {
            var isInRegion = await _context.EmployeeRegions.Where(e => e.EmployeeId == entity.EmployeeId && engageRegionIds.Contains(e.EngageRegionId))
                                                           .AnyAsync(cancellationToken);

            if (!isInRegion)
            {
                throw new Exception("This Employee is not in your Region");
            }
        }

        EmployeeAssetsVM employeeVM = new EmployeeAssetsVM() { HasAssets = false };

        _mapper.Map(entity, employeeVM);

        var assets = await _context.EmployeeAssets.Where(e => e.EmployeeId == request.Id)
            .AsNoTracking()
            .ProjectTo<EmployeeAssetVM>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (assets.Any())
        {
            foreach (var asset in assets)
            {
                asset.AssetType = "Electronics";
            }
            employeeVM.Assets.AddRange(assets);
            employeeVM.HasAssets = true;
        }

        //get non-personal vehicles
        var vehicles = await _context.EmployeeVehicles.Where(e => e.EmployeeId == request.Id && e.AssetOwnerId != (int)AssetOwnerId.Peronal)
            .AsNoTracking()
            .ProjectTo<EmployeeAssetVM>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (vehicles.Count != 0)
        {
            foreach (var vehicle in vehicles)
            {
                vehicle.AssetType = "Vehicles";
            }
            employeeVM.Assets.AddRange(vehicles);
            employeeVM.HasAssets = true;
        }

        var coolerBoxes = await _context.EmployeeCoolerBoxes.Where(e => e.EmployeeId == request.Id)
            .AsNoTracking()
            .ProjectTo<EmployeeAssetVM>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (coolerBoxes.Any())
        {
            foreach (var cooler in coolerBoxes)
            {
                cooler.AssetType = "Cooler Boxes";
            }
            employeeVM.Assets.AddRange(coolerBoxes);
            employeeVM.HasAssets = true;
        }

        return employeeVM;
    }
}
