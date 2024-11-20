using Engage.Application.Services.UserGroups.Models;

namespace Engage.Application.Services.UserGroups.Queries
{
    public class UserGroupQuery : IRequest<ListResult<UserGroupDto>>
    {
        public int? UserGroupId { get; set; }
    }

    public class UserGroupHandler : BaseQueryHandler, IRequestHandler<UserGroupQuery, ListResult<UserGroupDto>>
    {
        public UserGroupHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<UserGroupDto>> Handle(UserGroupQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.UserGroups.IgnoreQueryFilters().Where(e => !e.Deleted && !e.Deleted).AsQueryable();

            if (request.UserGroupId.HasValue)
            {
                queryable = queryable.Where(e => e.UserGroupId == request.UserGroupId.Value);
            }

            var entities = await queryable.ProjectTo<UserGroupDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new ListResult<UserGroupDto>(entities);
        }
    }
}