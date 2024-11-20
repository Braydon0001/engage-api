namespace Engage.Application.Services.UserUserGroups.Commands;
using Okta.Sdk;
using Okta.Sdk.Configuration;

public class UserUserGroupDeleteCommand : IRequest<OperationStatus>
{
    public int UserId { get; set; }
    public int UserGroupId { get; set; }
}

public class UserUserGroupDeleteCommandHandler : IRequestHandler<UserUserGroupDeleteCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IOptions<JwtOptions> _options;

    public UserUserGroupDeleteCommandHandler(IAppDbContext context, IBlobService blob, IOptions<JwtOptions> options)
    {
        _context = context;
        _options = options;
    }

    public async Task<OperationStatus> Handle(UserUserGroupDeleteCommand request, CancellationToken cancellationToken)
    {
        var client = new OktaClient(new OktaClientConfiguration
        {
            OktaDomain = _options.Value.Authority,
            Token = _options.Value.UsersApiToken
        });

        var oktaUser = await client.Users.GetUserAsync(_context.Users.Where(e => e.UserId == request.UserId).Select(e => e.Email).FirstOrDefault());
        var oktaGroup = await _context.UserGroups.Where(e => e.UserGroupId == request.UserGroupId).Select(e => e.VendorId).FirstOrDefaultAsync();

        if (oktaGroup != null && oktaUser != null)
        {
            await client.Groups.RemoveUserFromGroupAsync(oktaGroup, oktaUser.Id);
        }

        var entity = await _context.UserUserGroups.IgnoreQueryFilters().SingleAsync(e => e.UserId == request.UserId && e.UserGroupId == request.UserGroupId && !e.Disabled && !e.Deleted, cancellationToken);
        _context.UserUserGroups.Remove(entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        return operationStatus;
    }
}
