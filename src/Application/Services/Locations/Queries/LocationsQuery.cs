using Engage.Application.Services.Locations.Models;
using Engage.Application.Services.Stakeholders;

namespace Engage.Application.Services.Locations.Queries
{
    public class LocationsQuery : GetQuery, IRequest<ListResult<LocationListItemDto>>
    {
        public StakeholderTypes StakeholderType { get; set; }
        public int EntityId { get; set; }
    }

    public class LocationsQueryHandler : BaseListQueryHandler, IRequestHandler<LocationsQuery, ListResult<LocationListItemDto>>
    {
        private readonly IMediator _mediator;

        public LocationsQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
        {
            _mediator = mediator;
        }

        public async Task<ListResult<LocationListItemDto>> Handle(LocationsQuery request, CancellationToken cancellationToken)
        {
            var stakeholderId = await StakeholderUtils.GetIdForType(_mediator, request.StakeholderType, request.EntityId);

            var entities = await _context.Locations.Where(e => e.StakeholderId == stakeholderId)
                                                   .OrderBy(e => e.LocationId)
                                                   .ProjectTo<LocationListItemDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);

            return new ListResult<LocationListItemDto>(entities);
        }
    }
}
