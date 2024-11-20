namespace Engage.Application.Services.UserGroups.Queries;

public class UserGroupOptionQuery : IRequest<List<OptionDto>>
{
}

public record UserOrganizationOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserGroupOptionQuery, List<OptionDto>>
{
    public async Task<List<OptionDto>> Handle(UserGroupOptionQuery query, CancellationToken cancellationToken)
    {
        return await Context.UserGroups.IgnoreQueryFilters().Where(e => e.Name != "Everyone" && !e.Deleted && !e.Disabled)
                                       .Select(u => new OptionDto() { Name = u.Name, Id = u.UserGroupId, Disabled = u.Disabled })
                                       .ToListAsync(cancellationToken);
    }
}