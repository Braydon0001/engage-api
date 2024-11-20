using Engage.Application.Services.EmployeeAssets.Models;

namespace Engage.Application.Services.EmployeeAssets.Queries;

public class EmployeeAssetVmQuery : IRequest<EmployeeAssetVm>
{
    public int Id { get; set; }
}

public class EmployeeAssetVMQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeAssetVmQuery, EmployeeAssetVm>
{
    public EmployeeAssetVMQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeAssetVm> Handle(EmployeeAssetVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeAssets.Include(e => e.Employee)
                                                  .Include(e => e.EmployeeAssetType)
                                                  .Include(e => e.EmployeeAssetBrand)
                                                  .Include(e => e.AssetStatus)
                                                  .SingleAsync(x => x.EmployeeAssetId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeAsset, EmployeeAssetVm>(entity);
    }
}
