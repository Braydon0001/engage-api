using Engage.Application.Services.Locations.Models;

namespace Engage.Application.Services.Locations.Queries;

public class LocationVmQuery : IRequest<LocationVm>
{
    public int Id { get; set; }
}

public class LocationVmQueryHandler : BaseQueryHandler, IRequestHandler<LocationVmQuery, LocationVm>
{
    public LocationVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<LocationVm> Handle(LocationVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Locations.Include(e => e.LocationType)
                                             .Include(e => e.EngageLocation)
                                             .Include(e => e.EngageRegion)
                                             .SingleAsync(e => e.LocationId == request.Id, cancellationToken);

        return _mapper.Map<Location, LocationVm>(entity);
    }
}
