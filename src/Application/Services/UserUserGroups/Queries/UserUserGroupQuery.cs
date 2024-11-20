using Engage.Application.Services.UserUserGroups.Models;

namespace Engage.Application.Services.UserUserGroups.Queries
{
    public class UserUserGroupQuery : IRequest<List<UserUserGroupDto>>
    {
        public int? UserId { get; set; }
        public int? UserGroupId { get; set; }
    }

    public class UserUserGroupsQueryHandler : BaseQueryHandler, IRequestHandler<UserUserGroupQuery, List<UserUserGroupDto>>
    {
        public UserUserGroupsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<UserUserGroupDto>> Handle(UserUserGroupQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.UserUserGroups.IgnoreQueryFilters().AsQueryable().Where(e => !e.Deleted && !e.Disabled);

            if (request.UserId.HasValue)
            {
                queryable = queryable.Where(e => e.UserId == request.UserId.Value);
            }
            else if (request.UserGroupId.HasValue)
            {
                queryable = queryable.Where(e => e.UserGroupId == request.UserGroupId.Value);
            }

            var entities = await queryable.ProjectTo<UserUserGroupDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<UserUserGroupDto>(entities);
        }
    }
}