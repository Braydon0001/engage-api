using Engage.Application.Services.ClaimTypes.Models;

namespace Engage.Application.Services.ClaimTypes.Queries;

public class ClaimTypesQuery : GetQuery, IRequest<ListResult<ClaimTypeDto>>
{
}

public class ClaimTypesQueryHandler : BaseQueryHandler, IRequestHandler<ClaimTypesQuery, ListResult<ClaimTypeDto>>
{
    public ClaimTypesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<ClaimTypeDto>> Handle(ClaimTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.ClaimTypes.OrderBy(e => e.ClaimTypeId)
                                                .ProjectTo<ClaimTypeDto>(_mapper.ConfigurationProvider)
                                                .ToListAsync(cancellationToken);

        return new ListResult<ClaimTypeDto>
        {
            Count = entities.Count,
            Data = entities
        };

    }
} 
