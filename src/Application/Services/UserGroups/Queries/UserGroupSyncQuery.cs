using Engage.Application.Services.UserGroups.Models;
using Okta.Sdk;
using Okta.Sdk.Configuration;

namespace Engage.Application.Services.UserGroups.Queries
{
    public class UserGroupSyncQuery : IRequest<List<UserGroupDto>>
    {
    }

    public class UserGroupSyncHandler : BaseQueryHandler, IRequestHandler<UserGroupSyncQuery, List<UserGroupDto>>
    {
        private readonly IOptions<JwtOptions> _options;
        public UserGroupSyncHandler(IAppDbContext context, IMapper mapper, IOptions<JwtOptions> options) : base(context, mapper)
        {
            _options = options;
        }

        public async Task<List<UserGroupDto>> Handle(UserGroupSyncQuery request, CancellationToken cancellationToken)
        {
            var client = new OktaClient(new OktaClientConfiguration
            {
                OktaDomain = _options.Value.Authority,
                Token = _options.Value.UsersApiToken
            });

            var groups = await client.Groups.ToListAsync();

            foreach (var group in groups)
            {
                var entity = new UserGroup
                {
                    Name = group.Profile.Name,
                    Description = group.Profile.Description,
                    VendorId = group.Id,
                };
                _context.UserGroups.Add(entity);
            }

            await _context.SaveChangesAsync(cancellationToken);

            var queryable = _context.UserGroups.IgnoreQueryFilters().AsQueryable().Where(e => !e.Deleted && !e.Disabled);

            var entities = await queryable.ProjectTo<UserGroupDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<UserGroupDto>(entities);
        }
    }
}