namespace Engage.Application.Services.UserGroups.Commands;
using Okta.Sdk;
using Okta.Sdk.Configuration;

public class UserGroupDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public bool? SkipOktaActions { get; set; } = false;
}

public class UserGroupDeleteCommandHandler : IRequestHandler<UserGroupDeleteCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IOptions<JwtOptions> _options;

    public UserGroupDeleteCommandHandler(IAppDbContext context, IMediator mediator, IOptions<JwtOptions> options)
    {
        _context = context;
        _mediator = mediator;
        _options = options;
    }

    public async Task<OperationStatus> Handle(UserGroupDeleteCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.UserGroups.IgnoreQueryFilters().SingleAsync(x => x.UserGroupId == request.Id && !x.Disabled && !x.Deleted, cancellationToken);

        if (request.SkipOktaActions == false)
        {
            var client = new OktaClient(new OktaClientConfiguration
            {
                OktaDomain = _options.Value.Authority,
                Token = _options.Value.UsersApiToken
            });

            var oktaUserGroup = await client.Groups.GetGroupAsync(group.VendorId, cancellationToken);

            if (oktaUserGroup != null)
            {
                await oktaUserGroup.DeleteAsync(cancellationToken);
            }
        }

        var groupMembers = await _context.UserUserGroups.IgnoreQueryFilters().Where(e => e.UserGroupId == request.Id && !e.Disabled && !e.Deleted).ToListAsync();

        foreach (var groupMember in groupMembers)
        {
            _context.UserUserGroups.Remove(groupMember);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return await _mediator.Send(new DeleteCommand
        {
            EntityName = "UserGroup",
            Id = request.Id,
        }, cancellationToken);
    }
}