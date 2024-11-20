namespace Engage.Application.Services.UserUserGroups.Commands;
using Okta.Sdk;
using Okta.Sdk.Configuration;

public class UserUserGroupBatchCreateCommand : IRequest<OperationStatus>
{
    public List<int> UserIds { get; set; }
    public int UserGroupId { get; set; }
}
public class UserUserGroupBatchCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<UserUserGroupBatchCreateCommand, OperationStatus>
{
    private readonly IOptions<JwtOptions> _options;
    public UserUserGroupBatchCreateCommandHandler(IAppDbContext context, IMapper mapper, IOptions<JwtOptions> options) : base(context, mapper)
    {
        _options = options;
    }

    public async Task<OperationStatus> Handle(UserUserGroupBatchCreateCommand request, CancellationToken cancellationToken)
    {
        var client = new OktaClient(new OktaClientConfiguration
        {
            OktaDomain = _options.Value.Authority,
            Token = _options.Value.UsersApiToken
        });

        foreach (var user in request.UserIds)
        {
            var oktaUser = await client.Users.GetUserAsync(_context.Users.Where(e => e.UserId == user).Select(e => e.Email).FirstOrDefault());
            var oktaGroup = await _context.UserGroups.Where(e => e.UserGroupId == request.UserGroupId).Select(e => e.VendorId).FirstOrDefaultAsync();

            if (oktaGroup != null && oktaUser != null)
            {
                await client.Groups.AddUserToGroupAsync(oktaGroup, oktaUser.Id);
            }

            var userUserGroupEntry = new UserUserGroup
            {
                UserGroupId = request.UserGroupId,
                UserId = user,
            };
            _context.UserUserGroups.Add(userUserGroupEntry);
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}

