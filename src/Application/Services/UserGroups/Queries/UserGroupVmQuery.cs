using Engage.Application.Services.UserGroups.Models;

namespace Engage.Application.Services.UserGroups.Queries
{
    public class UserGroupVmQuery : IRequest<UserGroupDto>
    {
        public int Id { get; set; }
    }

    public class UserGroupVmHandler : BaseQueryHandler, IRequestHandler<UserGroupVmQuery, UserGroupDto>
    {
        public UserGroupVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<UserGroupDto> Handle(UserGroupVmQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.UserGroups
                .SingleAsync(e => e.UserGroupId == request.Id, cancellationToken);

            var result = _mapper.Map<UserGroup, UserGroupDto>(entity);

            return result;
        }
    }
}