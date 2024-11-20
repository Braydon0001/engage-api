using Engage.Application.Services.EngageRegions.Models;

namespace Engage.Application.Services.EngageRegions.Queries;

public class EngageRegionVmQuery : GetByIdQuery, IRequest<EngageRegionVm>
{ }

public class EngageRegionVmQueryHandler : BaseQueryHandler, IRequestHandler<EngageRegionVmQuery, EngageRegionVm>
{
    public EngageRegionVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageRegionVm> Handle(EngageRegionVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageRegions.Include(e => e.StoreSparRegion).SingleAsync(x => x.Id == request.Id, cancellationToken);

        return _mapper.Map<EngageRegion, EngageRegionVm>(entity);
    }
}
