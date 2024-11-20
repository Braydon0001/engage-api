namespace Engage.Application.Services.UserGroups.Commands;
using Okta.Sdk;
using Okta.Sdk.Configuration;


public class UserGroupSyncCommand : IRequest<OperationStatus>
{
}

public class UserGroupSyncHandler : BaseCreateCommandHandler, IRequestHandler<UserGroupSyncCommand, OperationStatus>
{
    private readonly IOptions<JwtOptions> _options;
    public UserGroupSyncHandler(IAppDbContext context, IMapper mapper, IOptions<JwtOptions> options) : base(context, mapper)
    {
        _options = options;
    }

    public async Task<OperationStatus> Handle(UserGroupSyncCommand command, CancellationToken cancellationToken)
    {
        var client = new OktaClient(new OktaClientConfiguration
        {
            OktaDomain = _options.Value.Authority,
            Token = _options.Value.UsersApiToken
        });

        var groups = client.Groups.ListGroups();

        //foreach (var group in groups)
        //{

        //}

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}