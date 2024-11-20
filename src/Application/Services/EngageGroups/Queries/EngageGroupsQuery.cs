using Engage.Application.Services.EngageGroups.Models;

namespace Engage.Application.Services.EngageGroups.Queries;

public class EngageGroupsQuery : GetQuery, IRequest<ListResult<EngageGroupDto>>
{

}

public class EngageGroupsQueryHandler : BaseQueryHandler, IRequestHandler<EngageGroupsQuery, ListResult<EngageGroupDto>>
{
    public EngageGroupsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EngageGroupDto>> Handle(EngageGroupsQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.EngageGroups.OrderBy(e => e.Name)
                                                  .ProjectTo<EngageGroupDto>(_mapper.ConfigurationProvider)
                                                  .ToListAsync(cancellationToken);

        return new ListResult<EngageGroupDto>(entities);

    }
}
