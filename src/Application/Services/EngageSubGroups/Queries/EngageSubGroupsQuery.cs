using Engage.Application.Services.EngageSubGroups.Models;

namespace Engage.Application.Services.EngageSubGroups.Queries;

public class EngageSubGroupsQuery : GetQuery, IRequest<ListResult<EngageSubGroupDto>>
{

}

public class EngageSubGroupsQueryHandler : BaseQueryHandler, IRequestHandler<EngageSubGroupsQuery, ListResult<EngageSubGroupDto>>
{
    public EngageSubGroupsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EngageSubGroupDto>> Handle(EngageSubGroupsQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.EngageSubGroups.OrderBy(e => e.Name)
                                                     .ProjectTo<EngageSubGroupDto>(_mapper.ConfigurationProvider)
                                                     .ToListAsync(cancellationToken);

        return new ListResult<EngageSubGroupDto>(entities);

    }
} 
