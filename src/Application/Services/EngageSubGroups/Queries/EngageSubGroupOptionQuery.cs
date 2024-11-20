using Engage.Application.Services.EngageSubGroups.Models;

namespace Engage.Application.Services.EngageSubGroups.Queries;

public class EngageSubGroupOptionQuery : IRequest<List<EngageSubGroupOption>>
{
    public List<int> EngageGroupIds { get; set; }
}
public record EngageSubGroupOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageSubGroupOptionQuery, List<EngageSubGroupOption>>
{
    public async Task<List<EngageSubGroupOption>> Handle(EngageSubGroupOptionQuery request, CancellationToken cancellationToken)
    {
        var queryable = Context.EngageSubGroups.AsQueryable().AsNoTracking();

        if (request.EngageGroupIds != null && request.EngageGroupIds.Count > 0)
        {
            queryable = queryable.Where(e => request.EngageGroupIds.Contains(e.EngageGroupId));
        }

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EngageSubGroupOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}
