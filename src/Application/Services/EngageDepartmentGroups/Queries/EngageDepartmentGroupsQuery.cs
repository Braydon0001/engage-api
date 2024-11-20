using Engage.Application.Services.EngageDepartmentGroups.Models;

namespace Engage.Application.Services.EngageDepartmentGroups.Queries;

public class EngageDepartmentGroupsQuery : GetQuery, IRequest<ListResult<EngageDepartmentGroupDto>>
{

}

public class EngageDepartmentGroupsHandler : BaseQueryHandler, IRequestHandler<EngageDepartmentGroupsQuery, ListResult<EngageDepartmentGroupDto>>
{
    public EngageDepartmentGroupsHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EngageDepartmentGroupDto>> Handle(EngageDepartmentGroupsQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.EngageDepartmentGroups.OrderBy(e => e.Name)
                                                  .ProjectTo<EngageDepartmentGroupDto>(_mapper.ConfigurationProvider)
                                                  .ToListAsync(cancellationToken);

        return new ListResult<EngageDepartmentGroupDto>(entities);

    }
}
