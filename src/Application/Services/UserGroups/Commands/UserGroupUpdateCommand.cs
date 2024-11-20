namespace Engage.Application.Services.UserGroups.Commands;

using Okta.Sdk;
using Okta.Sdk.Configuration;

public class UserGroupUpdateCommand : UserGroupCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UserGroupUpdateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UserGroupUpdateCommand, OperationStatus>
{
    private readonly IOptions<JwtOptions> _options;
    public UserGroupUpdateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IOptions<JwtOptions> options) : base(context, mapper, mediator)
    {
        _options = options;
    }

    public async Task<OperationStatus> Handle(UserGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.UserGroups.IgnoreQueryFilters().SingleAsync(x => x.UserGroupId == command.Id && !x.Deleted && !x.Disabled, cancellationToken);
        _mapper.Map(command, entity);

        if (command.SkipOktaActions == false)
        {
            var client = new OktaClient(new OktaClientConfiguration
            {
                OktaDomain = _options.Value.Authority,
                Token = _options.Value.UsersApiToken
            });

            var group = await client.Groups.GetGroupAsync(entity.VendorId);

            if (group == null)
            {
                throw new Exception("Okta group not found");
            }

            group.Profile.Name = entity.Name;
            group.Profile.Description = entity.Description;

            await group.UpdateAsync();
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.UserGroupId;
        return opStatus;
    }
}



