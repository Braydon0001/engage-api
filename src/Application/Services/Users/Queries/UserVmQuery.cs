using Engage.Application.Services.Users.Models;

namespace Engage.Application.Services.Users.Queries;

public class UserVmQuery : GetByIdQuery, IRequest<UserVm>
{
}

public class UserVmQueryHandler : BaseQueryHandler, IRequestHandler<UserVmQuery, UserVm>
{
    public UserVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<UserVm> Handle(UserVmQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.Include(e => e.Supplier)
                                       .Include(e => e.Role)
                                       .Include(e => e.UserEngageSubGroups)
                                          .ThenInclude(e => e.EngageSubGroup)
                                            .ThenInclude(e => e.EngageGroup)
                                       .Include(e => e.UserCommunicationTypes)
                                           .ThenInclude(c => c.CommunicationType)
                                       .Include(e => e.UserRegions)
                                           .ThenInclude(c => c.EngageRegion)
                                       .SingleAsync(e => e.UserId == request.Id, cancellationToken);

        var groups = await _context.UserUserGroups.IgnoreQueryFilters().Where(e => !e.Deleted && !e.Disabled)
                                                  .Where(e => e.UserId == user.UserId)
                                                  .Join(_context.UserGroups,
                                                        userUserGroup => userUserGroup.UserGroupId,
                                                        userGroup => userGroup.UserGroupId,
                                                        (userUserGroup, userGroup) => new { userGroup })
                                                  .ToListAsync(cancellationToken);


        var entity = _mapper.Map<User, UserVm>(user);

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
            var engageGroupIds = await _context.EngageGroups.Where(e => engageGroups.Contains(e.Id))
                                                            .Select(e => new OptionDto(e.Id, e.Name))
                                                            .ToListAsync(cancellationToken);

            entity.EngageGroupIds = engageGroupIds;
        }

        var regionIds = await _context.UserRegions.Where(e => e.UserId == request.Id)
                                                  .Select(e => e.EngageRegionId)
                                                  .ToListAsync(cancellationToken);

        if (regionIds != null && regionIds.Count > 0)
        {
            var queryable = _context.EngageRegions.Where(e => regionIds.Contains(e.Id));

            queryable = queryable.OrderBy(e => e.Name);

            var userRegions = await queryable.Select(e => new OptionDto(e.Id, e.Name))
                                             .ToListAsync(cancellationToken);

            entity.EngageRegionIds = userRegions;
        }

        return entity;
    }
}