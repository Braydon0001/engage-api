using Engage.Application.Services.Users.Models;

namespace Engage.Application.Services.Users.Queries;

public class UserVmEmailQuery : IRequest<UserVm>
{
    public string Email { get; set; }
}

public record UserVmEmailQueryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserVmEmailQuery, UserVm>
{
    public async Task<UserVm> Handle(UserVmEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await Context.Users.Include(e => e.Supplier)
                                       .Include(e => e.Role)
                                       .Include(e => e.UserEngageSubGroups)
                                          .ThenInclude(e => e.EngageSubGroup)
                                            .ThenInclude(e => e.EngageGroup)
                                       .FirstOrDefaultAsync(e => e.Email == request.Email, cancellationToken);

        if (user == null)
        {
            return null;
        }

        var groups = await Context.UserUserGroups.IgnoreQueryFilters().Where(e => !e.Deleted && !e.Disabled)
                                                  .Where(e => e.UserId == user.UserId)
                                                  .Join(Context.UserGroups,
                                                        userUserGroup => userUserGroup.UserGroupId,
                                                        userGroup => userGroup.UserGroupId,
                                                        (userUserGroup, userGroup) => new { userGroup })
                                                  .ToListAsync(cancellationToken);


        var entity = Mapper.Map<User, UserVm>(user);

        entity.Groups = groups.OrderBy(e => e.userGroup.Name)
                              .Where(e => e.userGroup.Name != "Everyone")
                              .Select(e => new OptionDto(e.userGroup.UserGroupId, e.userGroup.Name))
                              .ToList();

        var engageGroups = user.UserEngageSubGroups.Select(e => e.EngageSubGroup)
                                                   .Select(e => e.EngageGroupId)
                                                   .Distinct()
                                                   .ToList();

        if (engageGroups.Count > 0)
        {
            engageGroups = engageGroups.Distinct().ToList();
            var engageGroupIds = await Context.EngageGroups.Where(e => engageGroups.Contains(e.Id))
                                                            .Select(e => new OptionDto(e.Id, e.Name))
                                                            .ToListAsync(cancellationToken);

            entity.EngageGroupIds = engageGroupIds;
        }

        return entity;
    }
}