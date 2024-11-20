using Engage.Application.Services.DistributionCenters.Models;

namespace Engage.Application.Services.DistributionCenters.Queries;

public class DistributionCentersQuery : GetQuery, IRequest<ListResult<DistributionCenterDto>>
{
}

public class DistributionCentersQueryHandler : BaseQueryHandler, IRequestHandler<DistributionCentersQuery, ListResult<DistributionCenterDto>>
{
    public DistributionCentersQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<DistributionCenterDto>> Handle(DistributionCentersQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.DistributionCenters.OrderByDescending(d => d.DistributionCenterId)
                                                         .ProjectTo<DistributionCenterDto>(_mapper.ConfigurationProvider)
                                                         .ToListAsync(cancellationToken);

        return new ListResult<DistributionCenterDto>(entities);
    }
}
