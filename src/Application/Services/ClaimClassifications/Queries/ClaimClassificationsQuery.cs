using Engage.Application.Services.ClaimClassifications.Models;

namespace Engage.Application.Services.ClaimClassifications.Queries;

public class ClaimClassificationsQuery : GetQuery, IRequest<ListResult<ClaimClassificationDto>>
{
}

public class ClaimClassificationsQueryHandler : BaseQueryHandler, IRequestHandler<ClaimClassificationsQuery, ListResult<ClaimClassificationDto>>
{
    public ClaimClassificationsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<ClaimClassificationDto>> Handle(ClaimClassificationsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.ClaimClassifications.OrderBy(e => e.ClaimClassificationId)
                                                          .ProjectTo<ClaimClassificationDto>(_mapper.ConfigurationProvider)
                                                          .ToListAsync(cancellationToken);

        return new ListResult<ClaimClassificationDto>(entities);

    }
}
