using Engage.Application.Services.Mobile.User.Models;

namespace Engage.Application.Services.Mobile.User.Queries
{
    public class GetUserGroupsQuery : IRequest<List<UserGroupDto>>
    {


    }

    public class GetUserGroupsQueryHandler : BaseQueryHandler, IRequestHandler<GetUserGroupsQuery, List<UserGroupDto>>
    {
        private readonly IUserService _user;
        public GetUserGroupsQueryHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
        {
            _user = user;
        }

        public async Task<List<UserGroupDto>> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken)
        {
            var userGroups = await _context.UserUserGroups.IgnoreQueryFilters().Where(e => e.User.Email == _user.UserName && !e.Disabled && !e.Deleted).ProjectTo<UserGroupDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);


            return new List<UserGroupDto>(userGroups);
        }
    }
}
