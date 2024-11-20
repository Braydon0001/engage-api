namespace Engage.Application.Services.UserGroups.Commands;
using Okta.Sdk;
using Okta.Sdk.Configuration;


public class UserGroupCreateCommand : UserGroupCommand, IRequest<OperationStatus>
{
}

public class UserGroupCreateHandler : BaseCreateCommandHandler, IRequestHandler<UserGroupCreateCommand, OperationStatus>
{
    private readonly IOptions<JwtOptions> _options;
    public UserGroupCreateHandler(IAppDbContext context, IMapper mapper, IOptions<JwtOptions> options) : base(context, mapper)
    {
        _options = options;
    }

    public async Task<OperationStatus> Handle(UserGroupCreateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<UserGroupCreateCommand, UserGroup>(command);

        if (command.SkipOktaActions == false)
        {
            var client = new OktaClient(new OktaClientConfiguration
            {
                OktaDomain = _options.Value.Authority,
                Token = _options.Value.UsersApiToken
            });

            var group = await client.Groups.CreateGroupAsync(new CreateGroupOptions()
            {
                Name = entity.Name,
                Description = entity.Description,
            }, cancellationToken);

            entity.VendorId = group.Id;
        }

        _context.UserGroups.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.UserGroupId;
        return opStatus;
    }
}