using Okta.Sdk;
using Okta.Sdk.Configuration;

namespace Engage.Application.Services.Users.Commands;

public class RemoveUserCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public bool? SkipOktaActions { get; set; } = false;
}

public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IOptions<JwtOptions> _options;

    public RemoveUserCommandHandler(IAppDbContext context, IMediator mediator, IOptions<JwtOptions> options)
    {
        _context = context;
        _mediator = mediator;
        _options = options;
    }

    public async Task<OperationStatus> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleAsync(x => x.UserId == request.Id, cancellationToken);

        if (request.SkipOktaActions == false)
        {
            var client = new OktaClient(new OktaClientConfiguration
            {
                OktaDomain = _options.Value.Authority,
                Token = _options.Value.UsersApiToken
            });

            var foundUsers = await client.Users
                            .ListUsers(search: $"profile.email eq \"" + user.Email + "\"")
                            .ToArrayAsync();


            if (foundUsers.Length == 1)
            {
                var oktaUser = foundUsers[0];
                var status = oktaUser.Status;
                if (status == "DEPROVISIONED")
                {
                    await oktaUser.ActivateAsync(true, cancellationToken);
                }
                else
                {
                    await oktaUser.DeactivateAsync(cancellationToken);
                }
            }
        }

        return await _mediator.Send(new DeleteCommand
        {
            EntityName = "User",
            Id = request.Id,
            Toggle = true
        }, cancellationToken);
    }
}